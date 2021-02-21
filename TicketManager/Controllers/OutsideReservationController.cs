﻿using System;
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
    public class OutsideReservationController : Controller
    {
        public TicketContext context;
        private readonly ILogger<OutsideReservationController> logger;
        private readonly UserManager<IdentityUser> userManager;

        public OutsideReservationController(TicketContext _context,
            ILogger<OutsideReservationController> _logger,
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
        public IActionResult CreateRange(string id, [FromForm] string input)
        {
            var drama = context.Dramas.AsNoTracking()
                .FirstOrDefault(d => d.Name == id);
            if (drama == null || drama.UserId != userManager.GetUserId(User))
            {
                logger.LogError($"{id}という公演は存在しません");
                return NotFound($"{id}という公演は存在しません");
            }

            var newReservations = ParseInput(input, id);
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

        private List<OutsideReservation> ParseInput(string input, string dramaName)
        {
            string rt = Environment.NewLine;
            string[] lines = input.Split(rt);
            List<OutsideReservation> ret = new List<OutsideReservation>();
            for (int i = 1; i < lines.Length; i++)
            {
                ret.Add(CreateFromOneLine(lines[i], dramaName));
            }

            return ret;
        }

        private OutsideReservation CreateFromOneLine(string input, string dramaName)
        {
            // 項目ごとに分ける
            string[] items = input.Split("\t");

            // 一列ずらす（新歓かどうかの分岐のため）
            items[8] = items[7];
            items[7] = items[6];
            items[6] = items[5];

            // 作る 
            OutsideReservation reservation = new OutsideReservation();
            reservation.Time = items[0];
            reservation.GuestName = items[1];
            reservation.Furigana = items[2];
            reservation.StageNum = context.Stages
                .FirstOrDefault(s => s.DramaName == dramaName && s.Time == items[3]).Num;
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