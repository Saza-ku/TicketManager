using System.Diagnostics;
using System.Linq;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private TicketContext context;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ILogger<HomeController> _logger, TicketContext _context,
            UserManager<IdentityUser> _userManager)
        {
            logger = _logger;
            context = _context;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            var stages = context.Stages.ToArray();
            foreach (Stage s in stages)
            {
                logger.LogInformation($"公演名: {s.DramaName}, 日時: {s.Time}"
                    );
            }

            var Dramas = context.Dramas.AsNoTracking()
                .Where(d => d.UserId == userManager.GetUserId(User))
                .ToArray();
            return View(Dramas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
