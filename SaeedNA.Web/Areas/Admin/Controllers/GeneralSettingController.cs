using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Application.Utilities;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class GeneralSettingController : Controller
    {
        #region Ctor

        private readonly IGeneralSettingService _generalSettingService;

        public GeneralSettingController(IGeneralSettingService generalSettingService)
        {
            _generalSettingService = generalSettingService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index(SettingFilterDTO filter)
        {
            filter.IsDefault=true;
            return View("Index",await _generalSettingService.FilterSetting(filter));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SettingCreateDTO setting, IFormFile logo, IFormFile fav)
        {
            if (ModelState.IsValid)
            {
                if (logo != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(logo.FileName);
                    var result = await logo.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) setting.SiteLogo = fileName;
                }

                if (fav != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fav.FileName);
                    var result = await fav.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) setting.SiteFavIcon = fileName;
                }

                await _generalSettingService.AddNewSetting(setting);
                return RedirectToAction(nameof(Index));
            }

            return View("Add",setting);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            return View("Edit",await _generalSettingService.GetSettingById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SettingGetSetDTO setting, IFormFile logo, IFormFile fav)
        {
            if (ModelState.IsValid)
            {
                if (logo != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(logo.FileName);
                    var result = await logo.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) setting.SiteLogo = fileName;
                }

                if (fav != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fav.FileName);
                    var result = await fav.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) setting.SiteFavIcon = fileName;
                }

                await _generalSettingService.EditSetting(setting);
                return RedirectToAction(nameof(Index));
            }

            return View("Edit",setting);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _generalSettingService.DeleteSetting(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "GeneralSetting not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "GeneralSetting was deleted!" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetDefault(long id)
        {
            var result = await _generalSettingService.SetDefaultSetting(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "GeneralSetting not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "GeneralSetting is now default!" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
