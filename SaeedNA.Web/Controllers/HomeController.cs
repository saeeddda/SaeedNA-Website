using Microsoft.AspNetCore.Mvc;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;
using SaeedNA.Web.Models;
using System.Diagnostics;

namespace SaeedNA.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor

        private readonly SettingManager _settingManager;

        public HomeController(ISiteSettings siteSettings)
        {
            _settingManager = new SettingManager(siteSettings);
        }

        #endregion

        #region HomeActions

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags;
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;

            //Profile Settings
            ViewBag.FullName = set.FullName;
            ViewBag.Birthday = set.Birthday;
            ViewBag.Mobile = set.Mobile;
            ViewBag.AboutMe = set.AboutMe;
            ViewBag.Slogans = set.Slogans;
            ViewBag.Address = set.Address;
            ViewBag.PhoneNumber = set.PhoneNumber;
            ViewBag.Email = set.Email;
            ViewBag.ResumeImage = set.ResumeImage;
            ViewBag.AvatarImage = set.AvatarImage;
            ViewBag.ResumeFile = set.ResumeFile;

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
