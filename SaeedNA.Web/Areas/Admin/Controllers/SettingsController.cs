using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Application.Utilities;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingsController : Controller
    {
        #region Ctor

        private readonly IPersonalInfoService _personalService;
        private readonly ISeoService _seoService;
        private readonly ISiteSettingService _settingService;
        private readonly ISocialMediaService _socialMediaService;

        public SettingsController(IPersonalInfoService personalService, ISeoService seoService, ISiteSettingService settingService, ISocialMediaService socialMediaService)
        {
            _personalService = personalService;
            _seoService = seoService;
            _settingService = settingService;
            _socialMediaService = socialMediaService;
        }
        
        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            ViewBag.Settings = await _settingService.GetDefaultSetting();
            ViewBag.PersonalInfos = await _personalService.GetDefaultInfo();
            ViewBag.Seo = await _seoService.GetDefaultSeo();
            
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSettings(
            IFormFile logo, IFormFile fav,
            IFormFile resumeImage, IFormFile avatarImage,
            IFormFile resumeFile)
        {
            if(ModelState.IsValid)
            {

                if(logo != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    await logo.UploadToServer(fileName, PathExtension.UploadPathServer,null,null);
                    //string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    //setting.SiteLogo = fileUrl;
                }

                if(fav != null)
                {
                    //string fileRandomName = await uploader.FileUpload(fav, path);
                    //string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    //setting.SiteFavIcon = fileUrl;
                }

                if(resumeImage != null)
                {
                    //string fileRandomName = await uploader.FileUpload(resumeImage, path);
                    //string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    //setting.ResumeImage = fileUrl;
                }

                if(avatarImage != null)
                {
                    //string fileRandomName = await uploader.FileUpload(avatarImage, path);
                    //string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    //setting.AvatarImage = fileUrl;
                }

                if(resumeFile != null)
                {
                    //string fileRandomName = await uploader.FileUpload(resumeFile, path);
                    //string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);

                    //setting.ResumeFile = fileUrl;
                }

                

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("sitestting", "مشکلی در ذخیره تنظیمات بوجود آمده!");

            return View("Index");
        }

        #endregion
    }
}
