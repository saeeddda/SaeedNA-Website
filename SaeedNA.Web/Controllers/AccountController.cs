using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.ViewModels;
using System.Threading.Tasks;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SettingManager _settingManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ISettingService siteSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _settingManager =new SettingManager(siteSettings);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags.Split(',');
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;

            if(_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var loginView = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginView);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            if(_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            login.ReturnUrl = returnUrl;
            ViewData["ReturnUrl"] = returnUrl;

            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, true);

                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                if(result.IsLockedOut)
                {
                    ViewData["LoginError"] = "حساب کاربری شما به دلیل تلاش ناموفق در ورود قفل شده است. بعداً مجدد تلاش کنید.";
                    return View(login);
                }

                ModelState.Clear();
                ModelState.AddModelError("", "نام کاربری و یا کلمه عبور شما صحیح نیست");
            }

            return View(login);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Register()
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags.Split(',');
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, register.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                ModelState.Clear();

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("account", error.Description);
                }
            }

            return View(register);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Json(true);
            }

            return Json("ایمیل وارد شده قبلا استفاده شده است.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> IsUserNameInUse(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(username == null)
            {
                return Json(true);
            }

            return Json("نام کاربری وارد شده از قبل استفاده شده!");
        }
    }
}
