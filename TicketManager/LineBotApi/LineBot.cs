using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicketManager.Controllers;
using TicketManager.Data;
using TicketManager.LineBotApi.Models;

namespace TicketManager.LineBotApi
{
    public class LineBot
    {
        public string AccessToken { get; }
        private readonly HttpClient httpClient;
        private readonly ILogger<LineBotController> logger;
        private readonly TicketContext context;
        private readonly string rt;

        private readonly string usage = @"コマンド一覧:
・予約追加
・予約確認
・予約変更
・予約消去
・残席確認

詳細な使用方法は以下のページをご覧ください。
http://ticket-manager-saza.herokuapp.com/Usage/Bot";

        public LineBot(string accessToken, TicketContext _context ,ILogger<LineBotController> _logger)
        {
            AccessToken = accessToken;
            httpClient = new HttpClient();
            logger = _logger;
            rt = Environment.NewLine;
            context = _context;
        }

        public async Task Run(Event ev)
        {
            try
            {
                switch (ev.type)
                {
                    case "message":
                        await ProcessText(ev.replyToken, ev.message.text, ev.source.userId);
                        break;
                    default:
                        break;
                }
            }
            catch(LineBotException ex)
            {
                await SendTextReplyAsync(ev.replyToken, ex.Message);
            }
            catch(Exception ex)
            {
                logger.LogError($"不明なエラー: {ev.message.text}");
                logger.LogError($"エラー内容: {ex.Message}");
                await SendTextReplyAsync(ev.replyToken, $"不明なエラーです。{rt}そうちゃんに教えてあげよう！");
            }
            
        }

        private async Task ProcessText(string replyToken, string text, string userId)
        {
            var items = text.Split(rt);
            var command = items[0];

            string message;

            switch (command)
            {
                case "団員予約":
                case "予約追加":
                    message = await LineBotUtil.AddInCorona(context, items, userId);
                    break;
                case "予約一覧":
                case "予約確認":
                    message = await LineBotUtil.Get(context, items, userId);
                    break;
                case "予約変更":
                case "予約編集":
                    message = await LineBotUtil.Edit(context, items, userId);
                    break;
                case "予約削除":
                case "予約キャンセル":
                    message = await LineBotUtil.Delete(context, items, userId);
                    break;
                case "通知登録":
                    await LineBotUtil.Register(context, userId);
                    message = "登録完了";
                    break;
                case "通知登録解除":
                    await LineBotUtil.Unregister(context, userId);
                    message = "登録解除";
                    break;
                case "残席確認":
                case "残席一覧":
                    message = await LineBotUtil.GetRemainingSeats(context, items);
                    break;
                case "起きてる？":
                case "おきてる？":
                case "起きてる":
                case "おきてる":
                    message = "起きてるよー！";
                    break;
                default:
                    message = usage;
                    break;
            }

            await SendTextReplyAsync(replyToken, message);
        }

        private async Task SendTextReplyAsync(string replyToken, string message)
        {
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", AccessToken);

            // 送る json を用意
            var reply = new LineTextReply()
            {
                replyToken = replyToken,
                messages = new List<Message>()
                {
                    new Message()
                    {
                        type = "text",
                        text = message
                    }
                }
            };
            var json = JsonConvert.SerializeObject(reply);

            // リクエストの内容を用意
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");

            logger.LogInformation(json);

            // リクエスト送信
            var response = await httpClient.PostAsync
                ("https://api.line.me/v2/bot/message/reply", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var res = await response.Content.ReadAsStringAsync();
                logger.LogError(res);
                throw new Exception(res);
            }
        }
    }
}

