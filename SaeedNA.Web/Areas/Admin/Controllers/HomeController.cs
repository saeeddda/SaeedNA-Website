using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Ctor

        private readonly ISiteSettingService _settingService;
        private readonly IPostService _post;

        public HomeController(ISiteSettingService settingService, IPostService post)
        {
            _settingService = settingService;
            _post = post;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
