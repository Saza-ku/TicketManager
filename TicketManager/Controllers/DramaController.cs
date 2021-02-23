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

namespace TicketManager.Controllers
{
    [Authorize]
    public class DramaController : Controller
    {
        private readonly ILogger<DramaController> logger;
        public TicketContext context { get; set; }
        private readonly UserManager<IdentityUser> userManager;

        public DramaController(ILogger<DramaController> _logger, TicketContext _context,
            UserManager<IdentityUser> _userManager)
        {
            logger = _logger;
            context = _context;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if(drama == null || userManager.GetUserId(User) != drama.UserId)
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }
            ViewData["DramaName"] = id;
            var model = context.Stages.Where(s => s.DramaName == id).ToArray();
            foreach(Stage stage in model)
            {
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

                stage.CountOfGuests = count;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            logger.LogInformation("公演作成のページを表示します");
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm]CreateDramaModel createDramaModel)
        {
            var num = createDramaModel.NumOfStage;

            if(!ModelState.IsValid || createDramaModel.DramaName == null)
            {
                ModelState.AddModelError(string.Empty, "公演名を入力してください。");
                return View();
            }

            if(context.Dramas.AsNoTracking().Any(d => d.Name == createDramaModel.DramaName))
            {
                ModelState.AddModelError(string.Empty, "その公演名はすでに使われています。");
                return View();
            }

            logger.LogInformation($"新しい公演を作成します: 名前={createDramaModel.DramaName}");
            // 公演を作成
            var newDrama = new Drama();
            var dramaName = createDramaModel.DramaName;
            newDrama.Name = dramaName;
            newDrama.IsShinkan = createDramaModel.IsShinkan;
            newDrama.UserId = userManager.GetUserId(User);
            context.Dramas.Add(newDrama);
            
            // ステージを作成
            var newStages = new Stage[num];
            for(int i = 0; i < num; i++)
            {
                var culture = System.Globalization.CultureInfo.GetCultureInfo("ja-JP");

                var newStage = new Stage();
                newStage.DramaName = dramaName;
                newStage.Drama = newDrama;
                newStage.Num = i + 1;
                // 東京での現在時刻を取
                var jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
                newStage.Time = TimeZoneInfo.ConvertTime
                    (DateTimeOffset.Now, jstZoneInfo).DateTime.ToString("M月d日(ddd)HH時", culture);
                newStages[i] = newStage;
            }
            context.Stages.AddRange(newStages);

            // 変更を DB に反映
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError($"公演の作成に失敗: {e.Message}");
            }

            // リダイレクト
            var path = $"/Stage/Edit/{dramaName}";
            path=String.Join("/",path.Split("/")
                .Select(s => System.Net.WebUtility.UrlEncode(s)));
            return Redirect(path);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if(drama == null)
            {
                return NotFound($"{id}という公演は存在しません");
            }

            return View(drama);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(string id)
        {
            var drama = context.Dramas
                .FirstOrDefault(d => d.Name == id);
            context.Remove(drama);
            context.SaveChanges();

            // リダイレクト
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Export(string id)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if(drama == null || drama.UserId != userManager.GetUserId(User))
            {
                return NotFound($"{drama.Name}という公演はありません");
            }
            // CSV ファイル名
            string csvFileName = $"{drama.Name}.csv";

            // メモリを確保する
            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            using (var csv = new CsvWriter(writer, new CultureInfo(0x0411, false)))
            {
                var memberReservations = context.MemberReservations
                    .AsNoTracking()
                    .Where(r => r.DramaName == id)
                    .OrderBy(r => r.StageNum)
                    .ToArray();
                var outsideReservations = context.OutsideReservations
                    .AsNoTracking()
                    .Where(r => r.DramaName == id)
                    .OrderBy(r => r.StageNum)
                    .ToArray();

                // 取得したデータを記録す
                csv.WriteField("氏名");
                csv.WriteField("フリガナ");
                csv.WriteField("ステージ");
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

                int i = 0, j = 0;
                for (int n = 1; true; n++)
                {
                    if(i >= memberReservations.Length
                        && j >= outsideReservations.Length)
                    {
                        break;
                    }
                    while (i < memberReservations.Length
                        && memberReservations[i].StageNum == n)
                    {
                        WriteMemberReservation(memberReservations[i], drama.IsShinkan, csv);
                        i++;
                    }
                    while(j < outsideReservations.Length
                        && outsideReservations[j].StageNum == n)
                    {
                        WriteOutsideReservation(outsideReservations[j], drama.IsShinkan, csv);
                        j++;
                    }
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
            csv.WriteField($"{r.StageNum}st");
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
            csv.NextRecord();
        }

        private void WriteOutsideReservation(OutsideReservation r, bool isShinkan, CsvWriter csv)
        {
            csv.WriteField($"{r.GuestName}");
            csv.WriteField($"{r.Furigana}");
            csv.WriteField($"{r.StageNum}st");
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