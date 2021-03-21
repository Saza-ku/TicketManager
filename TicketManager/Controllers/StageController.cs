using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManager.Data;
using TicketManager.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketManager.Controllers
{
    [Authorize]
    public class StageController : Controller
    {
        public TicketContext context;
        private readonly ILogger<StageController> logger;
        private readonly UserManager<IdentityUser> userManager;

        public StageController(TicketContext _context, ILogger<StageController> _logger,
            UserManager<IdentityUser> _userManager)
        {
            context = _context;
            logger = _logger;
            userManager = _userManager;
        }

        // GET: /<controller>/
        public IActionResult Index(string id, [FromQuery] int num)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }
            var stage = context.Stages.AsNoTracking()
                .FirstOrDefault(s => s.DramaName == id && s.Num == num);
            if (stage == null)
            {
                return NotFound($"{id}に{num}stはありません");
            }
            var fromMembers = context.MemberReservations.AsNoTracking()
                .Where(r => r.DramaName == id && r.StageNum == num)
                .ToArray();
            var fromOutside = context.OutsideReservations.AsNoTracking()
                .Where(r => r.DramaName == id && r.StageNum == num)
                .ToArray();

            // 予約数を計算する
            var memberReservations = context.MemberReservations
                    .Where(r => r.DramaName == id && r.StageNum == stage.Num)
                    .ToArray();
            var outsideReservations = context.OutsideReservations
                .Where(r => r.DramaName == id && r.StageNum == stage.Num)
                .ToArray();
            int count = 0;
            if (drama.IsShinkan)
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

            // モデルを作って渡す
            stage.CountOfGuests = count;
            var model = new StageViewModel();
            model.Stage = stage;
            model.MemberReservations = fromMembers;
            model.OutsideReservations = fromOutside;

            // ViewData で渡す
            ViewData["IsShinkan"] = context.Dramas
                .FirstOrDefault(d => d.Name == id)
                .IsShinkan;

            // TODO: 例外処理（model == null)

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            var Stages = context.Stages
                .Where(s => s.DramaName == id)
                .OrderBy(s => s.Num)
                .ToArray();

            // View に渡すモデルを作成
            var model = new EditStagesModel();
            model.Stages = Stages;
            ViewData["DramaName"] = id;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(string id,
            [Bind("Stages")]
            EditStagesModel editStagesModel)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            var oldStages = context.Stages.Where(s => s.DramaName == id)
                .OrderBy(s => s.Num).ToArray();
            var newStages = editStagesModel.Stages;
            for (int i = 0; i < newStages.Length; i++)
            {
                oldStages[i].Time = newStages[i].Time;
                oldStages[i].Max = newStages[i].Max;
            }
            try
            {
                context.UpdateRange(oldStages);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError($"ステージ情報の更新ができませんでした: {e.Message}");
                throw;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Export(string id, int n)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                return NotFound($"{id}という公演はありません");
            }
            if (!context.Stages.AsNoTracking()
                .Any(s => s.DramaName == id && s.Num == n))
            {
                return NotFound($"{id}に{n}stはありません");
            }
            // CSV ファイル名
            string csvFileName = $"{drama.Name}_{n}st.csv";

            // メモリを確保する
            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            using (var csv = new CsvWriter(writer, new CultureInfo(0x0411, false)))
            {
                var memberReservations = context.MemberReservations
                    .AsNoTracking()
                    .Where(r => r.DramaName == id && r.StageNum == n)
                    .ToArray();
                var outsideReservations = context.OutsideReservations
                    .AsNoTracking()
                    .Where(r => r.DramaName == id && r.StageNum == n)
                    .ToArray();

                // 取得したデータを記録す
                csv.WriteField("氏名");
                csv.WriteField("フリガナ");
                if (drama.IsShinkan)
                {
                    csv.WriteField("新入生");
                    csv.WriteField("新入生以外");
                }
                else
                {
                    csv.WriteField("人数");
                }
                csv.WriteField("団員名");
                csv.WriteField("メールアドレス");
                csv.WriteField("連絡先");
                csv.WriteField("備考");
                csv.NextRecord();
                foreach (MemberReservation r in memberReservations)
                {
                    WriteMemberReservation(r, drama.IsShinkan, csv);
                }
                foreach(OutsideReservation r in outsideReservations)
                {
                    WriteOutsideReservation(r, drama.IsShinkan, csv);
                }

                writer.Flush();
                logger.LogInformation("CSVを出力します");

                // CSVファイルとして出力する
                return File(memory.ToArray(), "text/csv", csvFileName);
            }
        }

        private void WriteMemberReservation(MemberReservation r, bool isShinkan, CsvWriter csv)
        {
            csv.WriteField($"{r.GuestName}");
            csv.WriteField($"{r.Furigana}");
            if (isShinkan)
            {
                csv.WriteField($"{r.NumOfFreshmen}");
                csv.WriteField($"{r.NumOfOthers}");
            }
            else
            {
                csv.WriteField($"{r.NumOfGuests}");
            }
            csv.WriteField($"{r.MemberName}");
            csv.WriteField($"{r.Email}");
            csv.WriteField($"{r.PhoneNumber}");
            csv.NextRecord();
        }

        private void WriteOutsideReservation(OutsideReservation r, bool isShinkan, CsvWriter csv)
        {
            csv.WriteField($"{r.GuestName}");
            csv.WriteField($"{r.Furigana}");
            if (isShinkan)
            {
                csv.WriteField($"{r.NumOfFreshmen}");
                csv.WriteField($"{r.NumOfOthers}");
            }
            else
            {
                csv.WriteField($"{r.NumOfGuests}");
            }
            csv.WriteField("");
            csv.WriteField($"{r.Email}");
            csv.WriteField($"{r.PhoneNumber}");
            csv.WriteField($"{r.Remarks}");
            csv.NextRecord();
        }
    }
}
