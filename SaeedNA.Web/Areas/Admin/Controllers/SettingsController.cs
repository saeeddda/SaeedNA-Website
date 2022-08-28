using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Application.Utilities;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using SaeedNA.Service.Interfaces;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Application.ViewModels;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingsController : Controller
    {
        //TODO : Create site setting single page
        //TODO : Create profile setting single page
        //TODO : Modify site setting service and repository
        //TODO : Modify profile setting service and repository
        //TODO : Modify seo setting service and repository
        //TODO : Change all setting use models 
        //TODO : Create seo and sitemap settings 
        //TODO : Create google recaptcha settings 

        #region Ctor

        private readonly IPersonalInfoService _personalService;
        private readonly ISeoService _seoService;
        private readonly IGeneralSettingService _settingService;
        private readonly ISocialMediaService _socialMediaService;
        private readonly IConfiguration _configuration;

        public SettingsController(IPersonalInfoService personalService, ISeoService seoService, IGeneralSettingService settingService, ISocialMediaService socialMediaService, IConfiguration configuration)
        {
            _personalService = personalService;
            _seoService = seoService;
            _settingService = settingService;
            _socialMediaService = socialMediaService;
            _configuration = configuration;
        }

        #endregion

        

        #region Site Settings Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.HasSettings = false;

            var setting = await _settingService.GetDefaultSetting();
            var personalInfo = await _personalService.GetDefaultInfo();
            //var seo = await _seoService.GetDefaultSeo();

            var settings = new SiteSettingsViewModel
            {
                Setting = setting,
                PersonalInfo = personalInfo,
                //Seo = seo
            };

            if (setting != null && personalInfo != null)
            {
                ViewBag.HasSettings = true;

                var appSettings = AppSettingsManager.GetAppSettings(false);
                appSettings.SiteInstall = true;
                AppSettingsManager.SetAppSettings(appSettings, false);
            }

            return View("SiteSettings",settings);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SiteSettingsViewModel settings, IFormFile logo, IFormFile fav, IFormFile resumeImage, IFormFile avatarImage, IFormFile resumeFile)
        {
            if (ModelState.IsValid)
            {
                if (logo != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(logo.FileName);
                    var result = await logo.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) settings.Setting.SiteLogo = fileName;
                }

                if (fav != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fav.FileName);
                    var result = await fav.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) settings.Setting.SiteFavIcon = fileName;
                }

                if (resumeImage != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(resumeImage.FileName);
                    var result = await resumeImage.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) settings.PersonalInfo.ResumeImage = fileName;
                }

                if (avatarImage != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(avatarImage.FileName);
                    var result = await avatarImage.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) settings.PersonalInfo.AvatarImage = fileName;
                }

                if (resumeFile != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(resumeFile.FileName);
                    var result = await resumeFile.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) settings.PersonalInfo.ResumeFile = fileName;
                }

                await _personalService.SetDefaultPersonalInfo(settings.PersonalInfo);
                await _settingService.SetDefaultSetting(settings.Setting);
                //await _seoService.SetDefaultSeo(settings.Seo);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("page-account-settings", "مشکلی در ذخیره تنظیمات بوجود آمده!");

            return View("SiteSettings", settings);
        }

        #endregion
    }
}
