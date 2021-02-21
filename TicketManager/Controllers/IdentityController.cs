using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketManager.Models;

namespace TicketManager.Controllers
{
    [AllowAnonymous]
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<IdentityController> logger;
        public IdentityController(UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager,
            ILogger<IdentityController> _logger)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] IdInputModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {
                logger.LogInformation($"ログイン成功: {model.UserName}");
                return Redirect("/");
            }
            ModelState.AddModelError(string.Empty, "ログイン失敗");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(IdInputModel model)
        {
            var user = new IdentityUser() { UserName = model.UserName };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, true);
                return Redirect("/");
            }
            else
            {
                foreach (IdentityError e in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
            }
            return View();
        }

        [HttpGet]
        [ActionName("Logout")]
        public IActionResult LogoutView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            logger.LogInformation($"ログアウトします: {userManager.GetUserName(User)}");
            await signInManager.SignOutAsync();

            return Redirect("/");
        }
    }
}
