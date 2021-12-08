using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Framework.Configuration;
using SaeedNA.Framework.Utilities;
using SaeedNA.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OnlineUserController : Controller
    {
        #region Ctor

        private readonly SettingManager _settingManager;
        private readonly IOnlineUser _onlineUser;
        private PersianDateConvertor pdc = new PersianDateConvertor();

        public OnlineUserController(IOnlineUser onlineUser, ISettingService siteSettings)
        {
            _onlineUser = onlineUser;
            _settingManager = _settingManager = new SettingManager(siteSettings); 
        }

        #endregion

        #region Actions

        public IActionResult Index(int id = 1)
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var take = 20;
            var skip = (id - 1) * take;

            var onlineUserCount = _onlineUser.GetOnlineUserCount();

            ViewBag.PageCount = (onlineUserCount / take) + 1;
            ViewBag.Page = id;

            DateTime startdate = DateTime.Parse(DateTime.Now.Year + "/" + DateTime.Now.Month + "/01");

            var onlineUser = _onlineUser.GetOnlineUsersByDate(pdc.ConvertToShamsi(startdate), pdc.ConvertToShamsi(DateTime.Now), take, skip);
            //var onlineUser = _onlineUser.GetAllOnlineUsers();

            return View("Index", onlineUser);
        }

        #endregion
    }
}
