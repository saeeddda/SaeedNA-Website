using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Ctor

        private readonly SettingManager _settingManager;
        private readonly IPost _post;

        public HomeController(ISiteSettings settingManager, IPost post)
        {
            _settingManager = new SettingManager(settingManager);
            _post = post;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;
            ViewBag.PostCount = _post.GetAllPostCount();
            //ViewBag.IPAddress = HttpContext.Connection.

            return View();
        }

        #endregion
    }
}
