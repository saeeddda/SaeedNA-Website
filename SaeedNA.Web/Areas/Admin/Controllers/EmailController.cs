using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EmailController : Controller
    {
        #region Ctor

        private readonly IEmail _email;
        private readonly SettingManager _settingManager;

        public EmailController(IEmail email,ISettingService siteSetting)
        {
            _email = email;
            _settingManager = new SettingManager(siteSetting);
        }

        #endregion

        #region Email Actions

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var email = _email.GetAllEmail();
            return View("Index",email);
        }

        public IActionResult Detail(string id)
        {
            if(string.IsNullOrEmpty(id))
                return BadRequest();

            var email = _email.GetEmailById(int.Parse(id));

            if(email == null)
                return NotFound();

            return PartialView("Detail",email);
        }

        #endregion
    }
}
