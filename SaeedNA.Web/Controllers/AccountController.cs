using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Account;
using SaeedNA.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SaeedNA.Data.DTOs.Common;

namespace SaeedNA.Web.Controllers
{
    public class AccountController : Controller
    {
        #region constractor

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region login action

        [HttpGet("login/{returnUrl?}")]
        public IActionResult Login(string returnUrl = null)
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            var loginView = new LoginUserDTO() { ReturnUrl = returnUrl };

            return View(loginView);
        }

        [HttpPost("login/{returnUrl?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO login)
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            if(ModelState.IsValid)
            {
                var result = await _userService.GetUserForLogin(login);

                switch (result)
                {
                    case ServiceResult.Success:
                        var userData = await _userService.GetUserByUserName(login.UserName);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,userData.UserName),
                            new Claim(ClaimTypes.NameIdentifier,userData.Id.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };

                        await HttpContext.SignInAsync(principal, properties);

                        if(!string.IsNullOrEmpty(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl)) return Redirect(login.ReturnUrl);

                        return RedirectToAction("Index", "Home");

                    case ServiceResult.NotFond:
                        ViewData["LoginError"] = "نام کاربری و یا کلمه عبور شما صحیح نیست";
                        return View(login);

                    case ServiceResult.NotMatch:
                        ViewData["LoginError"] = "نام کاربری و یا کلمه عبور شما صحیح نیست";
                        return View(login);
                }
            }

            return View(login);
        }

        #endregion

        #region register action

        [HttpGet("register")]
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost("register"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO register)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(register);

                switch (result)
                {
                    case ServiceResult.Success:
                        return RedirectToAction("Login");
                    case ServiceResult.Exist:
                        ViewData["RegisterError"] = "نام کاربری و یا ایمیل قبلا استفاده شده است.";
                        return View(register);
                }
            }

            return View(register);
        }

        #endregion

        #region log out

        [HttpGet("log-out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
