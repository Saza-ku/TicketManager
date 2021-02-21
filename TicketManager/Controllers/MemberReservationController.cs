using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class MemberReservationController : Controller
    {
        public TicketContext context;
        private readonly ILogger<MemberReservationController> logger;
        private readonly UserManager<IdentityUser> userManager;

        public MemberReservationController(TicketContext _context,
            ILogger<MemberReservationController> _logger,
            UserManager<IdentityUser> _userManager)
        {
            context = _context;
            logger = _logger;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if(drama == null || drama.UserId != userManager.GetUserId(User))
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
        public IActionResult Create(string id, MemberReservation reservation)
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
            catch(Exception e)
            {
                logger.LogError($"予約登録に失敗しました: {e.Message}");
                return View();
            }

            var path = $"/Drama/Index/{id}";
            path = String.Join("/", path.Split("/")
                .Select(s => System.Net.WebUtility.UrlEncode(s)));
            return Redirect(path);
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

            var reservation = context.MemberReservations.AsNoTracking()
                .FirstOrDefault(r => r.Id == reservationId && r.DramaName == id);

            if(reservation == null)
            {
                logger.LogError
                    ($"団員予約情報が存在しません: DramaName={id}, Id={reservationId}");
                return NotFound("団員予約情報が存在しません");
            }

            ViewData["IsShinkan"] = drama.IsShinkan;
            return View(reservation);
        }

        [HttpPost]
        public IActionResult Edit(string id, [FromQuery] int reservationId,
            [ModelBinder] MemberReservation input)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound("団員予約情報が存在しません");
            }

            var reservation = context.MemberReservations.AsNoTracking()
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
            reservation.MemberName = input.MemberName;

            context.MemberReservations.Update(reservation);
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

            var reservation = context.MemberReservations
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == reservationId);

            if(reservation == null)
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

            var reservation = context.MemberReservations
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
            {
                logger.LogError($"団員予約情報が見つかりません: Id={reservationId}");
                return NotFound("団員予約情報が見つかりません。");
            }

            context.MemberReservations.Remove(reservation);
            context.SaveChanges();

            var path = $"/Drama/Index/{id}";
            path = String.Join("/", path.Split("/")
                .Select(s => System.Net.WebUtility.UrlEncode(s)));
            return Redirect(path);
        }
    }
}
