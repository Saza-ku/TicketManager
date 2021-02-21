using System;
using System.Security.Cryptography;
using System.Text;
using TicketManager.Data;
using TicketManager.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManager.LineBotApi.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace TicketManager.LineBotApi
{
    public class LineBotUtil
    {
        private static readonly string rt = Environment.NewLine;
        public static bool LineValidation(string signature, string text, string channelToken)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var keyBytes = Encoding.UTF8.GetBytes(channelToken);
            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                var hash = hmac.ComputeHash(textBytes, 0, textBytes.Length);
                var hash64 = Convert.ToBase64String(hash);

                return signature == hash64;
            }
        }

        public async static Task<string> Add(TicketContext context, string[] items, string userId)
        {
            if (items.Length < 2)
            {
                throw new LineBotException($"予約追加のメッセージフォーマットに従っていません。");
            }
            var drama = context.Dramas.FirstOrDefault(d => d.Name == items[1]);
            if (drama == null)
            {
                throw new LineBotException($"{items[1]}という公演は存在しません");
            }
            int itemCount = drama.IsShinkan ? 7 : 6;
            if (items.Length != itemCount)
            {
                throw new LineBotException($"予約追加のメッセージフォーマットに従っていません。");
            }
            

            // 予約を作成
            MemberReservation reservation = new MemberReservation();
            reservation.MemberId = userId;
            // コマンド, 公演名, 名前, ふりがな, st, 人数(新入生, それ以外) 
            reservation.DramaName = items[1];
            reservation.GuestName = items[2];
            reservation.Furigana = items[3];
            try
            {
                reservation.StageNum = int.Parse(items[4].Replace("st", ""));
                if (drama.IsShinkan)
                {
                    reservation.NumOfFreshmen = int.Parse(items[5].Replace("人", ""));
                    reservation.NumOfOthers = int.Parse(items[6].Replace("人", ""));
                    reservation.NumOfGuests = 0;
                }
                else
                {
                    reservation.NumOfGuests = int.Parse(items[5].Replace("人", ""));
                    reservation.NumOfFreshmen = 0;
                    reservation.NumOfOthers = 0;
                }
            }
            catch (FormatException)
            {
                throw new LineBotException($"予約追加のメッセージフォーマットに従っていません。");
            }

            // 予約を登録
            context.Add(reservation);
            await context.SaveChangesAsync();

            // 通知
            var stage = context.Stages.AsNoTracking()
                .FirstOrDefault(s => s.DramaName == drama.Name && s.Num == reservation.StageNum);
            UpdateCount(stage, drama.IsShinkan, context);
            var to = context.NotifiedMemberIds.AsNoTracking().Select(m => m.Id).ToArray();
            string message = $"予約が追加されました: {drama.Name}" + rt;
            message = message + $"{reservation.StageNum}st" + rt;
            message = message + $"{reservation.NumOfGuests + reservation.NumOfFreshmen + reservation.NumOfOthers}人" + rt;
            message = message + $"{reservation.StageNum}st: {stage.CountOfGuests}人 / {stage.Max}人";
            await SendMessage(message, to);

            // リターンする文字列を作成
            var ret = "予約を登録しました" + rt + rt;
            ret = ret + "公演名: " + reservation.DramaName + rt;
            ret = ret + "名前: " + reservation.DramaName + rt;
            ret = ret + "ステージ: " + reservation.StageNum + "st" + rt;
            if (drama.IsShinkan)
            {
                ret = ret + "新入生: " + reservation.NumOfFreshmen.ToString() + "人" + rt;
                ret = ret + "新入生以外: " + reservation.NumOfOthers.ToString() + "人" + rt;
            }
            else
            {
                ret = ret + "人数: " + reservation.NumOfGuests.ToString() + "人" + rt;
            }
            ret = ret + "予約ID: " + reservation.Id.ToString();

            return ret;
        }

        public async static Task<string> Get(TicketContext context, string[] items, string userId)
        {
            if (items.Length < 2)
            {
                throw new LineBotException("予約確認のフォーマットに従っていません。");
            }
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == items[1]);
            if (drama == null)
            {
                throw new LineBotException($"{items[1]}という公演は存在しません");
            }

            var reservations = await context.MemberReservations.AsNoTracking()
                .Where(r => r.DramaName == items[1] && r.MemberId == userId)
                .ToArrayAsync();

            // リターンする文字列を作成
            var ret = $"登録した予約一覧: {items[1]}";
            foreach (MemberReservation reservation in reservations)
            {
                ret = ret + rt + rt;
                ret = ret + "名前: " + reservation.GuestName + rt;
                ret = ret + "ステージ: " + reservation.StageNum + "st" + rt;
                if (drama.IsShinkan)
                {
                    ret = ret + "新入生: " + reservation.NumOfFreshmen + "人" + rt;
                    ret = ret + "新入生以外: " + reservation.NumOfOthers + "人" + rt;
                }
                else
                {
                    ret = ret + "人数: " + reservation.NumOfGuests + "人" + rt;
                }
                ret = ret + "予約ID: " + reservation.Id;
            }

            return ret;
        }

        public async static Task<string> Edit(TicketContext context, string[] items, string userId)
        {
            if (items.Length < 2)
            {
                throw new LineBotException("予約変更のフォーマットに従っていません。");
            }
            int id;
            try
            {
                id = int.Parse(items[1]);
            }
            catch (FormatException)
            {
                throw new LineBotException("予約変更のフォーマットに従っていません。");
            }

            var reservation = context.MemberReservations
                .FirstOrDefault(r => r.Id == id);
            if (reservation == null || reservation.MemberId != userId)
            {
                throw new LineBotException($"このような予約は存在しません{rt}予約ID: {id}");
            }

            var isShinkan = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == reservation.DramaName).IsShinkan;

            int itemCount = isShinkan ? 7 : 6;
            if (items.Length != itemCount)
            {
                throw new LineBotException($"予約追加のメッセージフォーマットに従っていません。");
            }

            // コマンド, ID, 名前, ふりがな, st, 人数(新入生, それ以外) 
            reservation.GuestName = items[2];
            reservation.Furigana = items[3];
            try
            {
                reservation.StageNum = int.Parse(items[4].Replace("st", ""));
                if (isShinkan)
                {
                    reservation.NumOfFreshmen = int.Parse(items[5].Replace("人", ""));
                    reservation.NumOfOthers = int.Parse(items[6].Replace("人", ""));
                }
                else
                {
                    reservation.NumOfGuests = int.Parse(items[5].Replace("人", ""));
                }
            }
            catch (FormatException)
            {
                throw new LineBotException($"予約追加のメッセージフォーマットに従っていません。");
            }

            // 予約更新
            context.MemberReservations.Update(reservation);
            await context.SaveChangesAsync();

            // リターンする文字列を作成
            var ret = "予約を削除しました" + rt + rt;
            ret = ret + "公演名: " + reservation.DramaName + rt;
            ret = ret + "名前: " + reservation.DramaName + rt;
            ret = ret + "ステージ: " + reservation.StageNum + "st" + rt;
            if (isShinkan)
            {
                ret = ret + "新入生: " + reservation.NumOfFreshmen.ToString() + "人" + rt;
                ret = ret + "新入生以外: " + reservation.NumOfOthers.ToString() + "人" + rt;
            }
            else
            {
                ret = ret + "人数: " + reservation.NumOfGuests.ToString() + "人" + rt;
            }
            ret = ret + "予約ID: " + reservation.Id.ToString();

            return ret;
        }

        public async static Task<string> Delete(TicketContext context, string[] items, string userId)
        {
            if (items.Length < 2)
            {
                throw new LineBotException("予約削除のフォーマットに従っていません。");
            }
            int id;
            try
            {
                id = int.Parse(items[1]);
            }
            catch (FormatException)
            {
                throw new LineBotException("予約削除のフォーマットに従っていません。");
            }

            var reservation = context.MemberReservations
                .FirstOrDefault(r => r.Id == id);
            if (reservation == null || reservation.MemberId != userId)
            {
                throw new LineBotException($"このような予約は存在しません{rt}予約ID: {id}");
            }

            // 予約削除
            context.Remove(reservation);
            await context.SaveChangesAsync();

            // 通知
            var isShinkan = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == reservation.DramaName)
                .IsShinkan;
            var stage = context.Stages.AsNoTracking()
                .FirstOrDefault(s => s.DramaName == reservation.DramaName && s.Num == reservation.StageNum);
            UpdateCount(stage, isShinkan, context);
            var to = context.NotifiedMemberIds.AsNoTracking().Select(m => m.Id).ToArray();
            string message = $"予約が削除されました: {reservation.DramaName}" + rt;
            message = message + $"{reservation.StageNum}: {stage.CountOfGuests}人 / {stage.Max}人";
            await SendMessage(message, to);

            // リターンする文字列を作成
            var ret = "予約を削除しました" + rt + rt;
            ret = ret + "公演名: " + reservation.DramaName + rt;
            ret = ret + "名前: " + reservation.DramaName + rt;
            ret = ret + "ステージ: " + reservation.StageNum + "st" + rt;
            if (isShinkan)
            {
                ret = ret + "新入生: " + reservation.NumOfFreshmen.ToString() + "人" + rt;
                ret = ret + "新入生以外: " + reservation.NumOfOthers.ToString() + "人" + rt;
            }
            else
            {
                ret = ret + "人数: " + reservation.NumOfGuests.ToString() + "人" + rt;
            }
            ret = ret + "予約ID: " + reservation.Id.ToString();

            return ret;
        }

        public async static Task Register(TicketContext context, string id)
        {
            context.NotifiedMemberIds.Add(new NotifiedMember() { Id = id });
            await context.SaveChangesAsync();
        }

        public async static Task Unregister(TicketContext context, string id)
        {
            context.NotifiedMemberIds.Remove(new NotifiedMember() { Id = id });
            await context.SaveChangesAsync();
        }

        private async static Task SendMessage(string text, string[] to)
        {
            if (to.Length == 0) return;
            var httpClient = new HttpClient();
            var message = new LineTextMessage()
            {
                to = to,
                messages = new Message[]
                {
                    new Message()
                    {
                        type = "text",
                        text = text
                    }
                }
            };
            var json = JsonConvert.SerializeObject(message);

            // リクエストの内容を用意
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.Add("Authorization",
                $"Bearer {Environment.GetEnvironmentVariable("ACCESS_TOKEN")}");

            // リクエスト送信
            var response = await httpClient.PostAsync
                ("https://api.line.me/v2/bot/message/multicast", content);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var res = await response.Content.ReadAsStringAsync();
                throw new Exception(res);
            }
        }

        private static void UpdateCount(Stage stage, bool isShinkan,TicketContext context)
        {
            var memberReservations = context.MemberReservations
                    .Where(r => r.DramaName == stage.DramaName && r.StageNum == stage.Num)
                    .ToArray();
            var outsideReservations = context.OutsideReservations
                .Where(r => r.DramaName == stage.DramaName&& r.StageNum == stage.Num)
                .ToArray();
            int count = 0;
            if (isShinkan)
            {
                foreach (MemberReservation r in memberReservations)
                {
                    count += r.NumOfFreshmen + r.NumOfOthers;
                }
                foreach (OutsideReservation r in outsideReservations)
                {
                    count += r.NumOfFreshmen + r.NumOfOthers;
                }
            }
            else
            {
                count += memberReservations.Select(r => r.NumOfGuests).Sum();
                count += outsideReservations.Select(r => r.NumOfGuests).Sum();
            }
            stage.CountOfGuests = count;
        }
    }
}
