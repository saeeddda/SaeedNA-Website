using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaeedNA.Framework.Configuration;
using SaeedNA.Framework.Utilities;
using SaeedNA.Service.Repositories;
using SaeedNA.Service.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingsController : Controller
    {
        #region Ctor

        private readonly SettingManager _settingManager;
        private Uploader uploader;

        public SettingsController(ISiteSettings siteSettings)
        {
            _settingManager = new SettingManager(siteSettings);
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

            List<SelectListItem> siteColor = new List<SelectListItem>() {
                new SelectListItem()
                {
                    Text="آبی",
                    Value="blue"
                },
                new SelectListItem()
                {
                    Text="نیلی",
                    Value="blueviolet"
                },
                new SelectListItem()
                {
                    Text="سبز",
                    Value="green"
                },
                new SelectListItem()
                {
                    Text="صورتی",
                    Value="magenta"
                },
                new SelectListItem()
                {
                    Text="نارنجی",
                    Value="orange"
                },
                new SelectListItem()
                {
                    Text="بنفش",
                    Value="purple"
                },
                new SelectListItem()
                {
                    Text="قرمز",
                    Value="red"
                },
                new SelectListItem()
                {
                    Text="زرد",
                    Value="yellow"
                },
                new SelectListItem()
                {
                    Text="زرد - سبز",
                    Value="yellogreen"
                },
                new SelectListItem()
                {
                    Text="طلایی",
                    Value="goldenrod"
                }
            };

            ViewBag.SiteColors = siteColor;

            List<SelectListItem> siteMode = new List<SelectListItem>(){
                new SelectListItem(){
                    Text="تاریک",
                    Value="dark"
                },
                new SelectListItem(){
                    Text="روشن",
                    Value="light"
                }
            };

            ViewBag.SiteMode = siteMode;

            return View("Index", set);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSettings(SiteSettingViewModel setting,
            IFormFile logo, IFormFile fav,
            IFormFile resumeImage, IFormFile avatarImage,
            IFormFile resumeFile)
        {
            if(ModelState.IsValid)
            {
                uploader = new Uploader();
                string uploadFolder = "Uploads";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadFolder);

                if(logo != null)
                {
                    string fileRandomName = await uploader.FileUpload(logo, path);
                    string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    setting.SiteLogo = fileUrl;
                }

                if(fav != null)
                {
                    string fileRandomName = await uploader.FileUpload(fav, path);
                    string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    setting.SiteFavIcon = fileUrl;
                }

                if(resumeImage != null)
                {
                    string fileRandomName = await uploader.FileUpload(resumeImage, path);
                    string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    setting.ResumeImage = fileUrl;
                }

                if(avatarImage != null)
                {
                    string fileRandomName = await uploader.FileUpload(avatarImage, path);
                    string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    setting.AvatarImage = fileUrl;
                }

                if(resumeFile != null)
                {
                    string fileRandomName = await uploader.FileUpload(resumeFile, path);
                    string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    setting.ResumeFile = fileUrl;
                }

                _settingManager.SetAllSetting(setting);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("sitestting", "مشکلی در ذخیره تنظیمات بوجود آمده!");

            return View("Index", setting);
        }

        #endregion
    }
}
