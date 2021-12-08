using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.Repositories;
using SaeedNA.Service.ViewModels;

namespace SaeedNA.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistory _experience;
        private readonly ISettingService _siteSettings;

        public HistoryController(
            IHistory experience,
            ISettingService siteSettings)
        {
            _experience = experience;
            _siteSettings = siteSettings;
        }


        public IActionResult Index()
        {
            ViewData["SiteFav"] = "/uploads/" + _siteSettings.GetSetting("SiteFavIcon");
            ViewData["SiteUrl"] = $"{this.Request.Scheme}://{this.Request.Host.Value}";


            return View("Index");
        }
    }
}
