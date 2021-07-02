using Microsoft.AspNetCore.Mvc;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;
using SaeedNA.Service.ViewModels;

namespace SaeedNA.Web.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly IHistory _history;
        private readonly ISkill _skill;
        private readonly IServiceCounter _serviceCounter;
        private readonly IMyService _myService;
        private readonly SettingManager _settingManager;

        public AboutMeController(
            IHistory history,
            ISkill skill,
            IServiceCounter serviceCounter,
            IMyService myService,
            ISiteSettings siteSettings)
        {
            _history = history;
            _skill = skill;
            _serviceCounter = serviceCounter;
            _myService = myService;
            _settingManager = new SettingManager(siteSettings);
        }

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

            AboutMeViewModel aboutMeViewModel = new AboutMeViewModel()
            {
                History = _history.GetAllHistory(),
                Skill = _skill.GetAllSkill(),
                ServiceCounter = _serviceCounter.GetAllServiceCounter(),
                MyService = _myService.GetAllMyService(),
                SiteSettings = set
            };

            return View("Index",aboutMeViewModel);
        }
    }
}
