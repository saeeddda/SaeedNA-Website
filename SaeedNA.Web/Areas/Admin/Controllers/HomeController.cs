using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;
using System;
using SaeedNA.Framework.Utilities;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Ctor

        private readonly SettingManager _settingManager;
        private readonly IPostService _post;
        private readonly IOnlineUser _onlineUser;
        private readonly PersianDateConvertor pdc = new PersianDateConvertor();

        public HomeController(ISettingService settingManager, IPostService post, IOnlineUser onlineUser)
        {
            _settingManager = new SettingManager(settingManager);
            _post = post;
            _onlineUser = onlineUser;
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

            DateTime startdate = DateTime.Parse(DateTime.Now.Year + "/" + DateTime.Now.Month + "/01");

            ViewBag.OnlineUserPerDay = _onlineUser.GetOnlineUserCount(pdc.ConvertToShamsi(DateTime.Now), pdc.ConvertToShamsi(DateTime.Now));
            ViewBag.OnlineUserPerMounth = _onlineUser.GetOnlineUserCount(pdc.ConvertToShamsi(startdate), pdc.ConvertToShamsi(DateTime.Now));

            return View();
        }

        #endregion
    }
}
