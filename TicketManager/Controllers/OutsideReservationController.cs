using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using TicketManager.Data;
using TicketManager.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketManager.Controllers
{
    [Authorize]
    public class OutsideReservationController : Controller
    {
        public TicketContext context;
        private readonly ILogger<OutsideReservationController> logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly Encoding shiftJis;

        public OutsideReservationController(TicketContext _context,
            ILogger<OutsideReservationController> _logger,
            UserManager<IdentityUser> _userManager)
        {
            context = _context;
            logger = _logger;
            userManager = _userManager;

            // shift_jis を追加
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            shiftJis = Encoding.GetEncoding("Shift_JIS");
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            ViewData["DramaName"] = id;
            ViewData["IsShinkan"] = context.Dramas
                .FirstOrDefault(d => d.Name == id)
                .IsShinkan;
            return View();
        }

        [HttpPost]
        public IActionResult Create(string id, OutsideReservation reservation)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            reservation.DramaName = id;
            if (reservation.StageNum == 0 ||
                reservation.GuestName == "")
            {
                ViewData["DramaName"] = id;
                ViewData["IsShinkan"] = context.Dramas
                    .FirstOrDefault(d => d.Name == id)
                    .IsShinkan;
                return View();
            }

            context.Add(reservation);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError($"予約登録に失敗しました: {e.Message}");
                logger.LogError($"Id: {reservation.Id}, Drama: {reservation.DramaName}");
                throw;
            }

            var path = $"/Drama/Index/{id}";
            path = String.Join("/", path.Split("/")
                .Select(s => System.Net.WebUtility.UrlEncode(s)));

            return Redirect(path);
        }

        [HttpGet]
        public IActionResult CreateRange(string id)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            ViewData["DramaName"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult CreateRange(string id, [FromForm] IFormFile file)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            // 入力をパースして予約情報を取得
            var newReservations = new List<OutsideReservation>();
            using (var stream = file.OpenReadStream())
            using (var parser = new TextFieldParser(stream, shiftJis))
            {
                // カンマ区切りの指定
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                // フィールドが引用符で囲まれているか
                parser.HasFieldsEnclosedInQuotes = true;
                // フィールドの空白トリム設定
                parser.TrimWhiteSpace = false;

                bool first = true;

                // ファイルの終端までループ
                while (!parser.EndOfData)
                {
                    // フィールドを読込
                    string[] row = parser.ReadFields();
                    if(first)
                    {
                        first = false;
                        continue;
                    }
                    OutsideReservation r;
                    try
                    {
                        r = CreateFromRow(row, drama.Name);
                    }
                    catch (NullReferenceException e)
                    {
                        return NotFound(e.Message);
                    }
                    newReservations.Add(r);
                }
            }

            foreach (OutsideReservation reservation in newReservations)
            {
                bool registerd = context.OutsideReservations.AsNoTracking()
                    .Any(r => r.Time == reservation.Time && r.GuestName == r.GuestName);
                if (!registerd)
                {
                    context.Add(reservation);
                }
            }
            logger.LogInformation($"予約数: {newReservations.Count}");
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError($"一括登録に失敗: {e.Message}");
                throw;
            }

            var path = $"/Drama/Index/{id}";
            path = String.Join("/", path.Split("/")
                .Select(s => System.Net.WebUtility.UrlEncode(s)));

            return Redirect(path);
        }

        private OutsideReservation CreateFromRow(string[] items, string dramaName)
        {
            logger.LogInformation("一括予約を行います\n" +
                $"公演名: {dramaName}" +
                $"情報: {string.Join(",", items)}");

            // 一列ずらす（新歓かどうかの分岐のため）
            items[8] = items[7];
            items[7] = items[6];
            items[6] = items[5];

            var stage = context.Stages
                .FirstOrDefault(s => s.DramaName == dramaName && s.Time == items[3]);

            if (stage == null)
            {
                logger.LogError($"ステージが見つかりません: 公演名={dramaName}, 日時: {items[3]}");
                throw new NullReferenceException($"ステージが見つかりません: 公演名 ={ dramaName }, 日時: { items[3]}");
            }

            // 作る 
            OutsideReservation reservation = new OutsideReservation();
            reservation.Time = items[0];
            reservation.GuestName = items[1];
            reservation.Furigana = items[2];
            reservation.StageNum = stage.Num;
            if (items[5].Contains("名"))
            {
                reservation.NumOfFreshmen = int.Parse(items[4].Replace("名", ""));
                reservation.NumOfOthers = int.Parse(items[5].Replace("名", ""));
            }
            else
            {
                reservation.NumOfGuests = int.Parse(items[4].Replace("名", ""));
            }
            reservation.Email = items[6];
            reservation.PhoneNumber = items[7];
            reservation.Remarks = items[8];
            reservation.DramaName = dramaName;

            return reservation;
        }

        [HttpGet]
        public IActionResult Edit(string id, [FromQuery] int reservationId)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            var reservation = context.OutsideReservations.AsNoTracking()
                .FirstOrDefault(r => r.Id == reservationId && r.DramaName == id);

            if (reservation == null)
            {
                logger.LogError
                    ($"一般予約情報が存在しません: DramaName={id}, Id={reservationId}");
                return NotFound("一般予約情報が存在しません");
            }

            ViewData["IsShinkan"] = drama.IsShinkan;
            return View(reservation);
        }

        [HttpPost]
        public IActionResult Edit(string id, [FromQuery] int reservationId,
            [ModelBinder] OutsideReservation input)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound("団員予約情報が存在しません");
            }

            var reservation = context.OutsideReservations.AsNoTracking()
                .FirstOrDefault(r => r.Id == reservationId && r.DramaName == id);
            if (reservation == null)
            {
                logger.LogError
                    ($"団員予約情報が存在しません: DramaName={id}, Id={reservationId}");
                return NotFound("団員予約情報が存在しません");
            }

            reservation.GuestName = input.GuestName;
            reservation.Furigana = input.Furigana;
            reservation.NumOfFreshmen = input.NumOfFreshmen;
            reservation.NumOfGuests = input.NumOfGuests;
            reservation.NumOfOthers = input.NumOfOthers;
            reservation.StageNum = input.StageNum;
            reservation.Email = input.Email;
            reservation.PhoneNumber = input.PhoneNumber;
            reservation.Remarks = input.Remarks;

            context.OutsideReservations.Update(reservation);
            context.SaveChanges();

            var path = $"/Drama/Index/{id}";
            path = String.Join("/", path.Split("/")
                .Select(s => System.Net.WebUtility.UrlEncode(s)));
            return Redirect(path);
        }

        [HttpGet]
        public IActionResult Delete(string id, [FromQuery] int reservationId)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound("団員予約情報が存在しません");
            }

            var reservation = context.OutsideReservations
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
            {
                logger.LogError($"団員予約情報が見つかりません: Id={reservationId}");
                return NotFound("団員予約情報が見つかりません。");
            }

            ViewData["IsShinkan"] = drama.IsShinkan;
            return View(reservation);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(string id, [FromQuery] int reservationId)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound("団員予約情報が存在しません");
            }

            var reservation = context.OutsideReservations
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
            {
                logger.LogError($"団員予約情報が見つかりません: Id={reservationId}");
                return NotFound("団員予約情報が見つかりません。");
            }

            context.OutsideReservations.Remove(reservation);
            context.SaveChanges();

            var path = $"/Drama/Index/{id}";
            path = String.Join("/", path.Split("/")
                .Select(s => System.Net.WebUtility.UrlEncode(s)));
            return Redirect(path);
        }
    }
}
