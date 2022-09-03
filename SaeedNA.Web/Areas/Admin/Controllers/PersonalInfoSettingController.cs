using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Application.Utilities;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class PersonalInfoSettingController : Controller
    {
        #region Ctor

        private readonly IPersonalInfoService _personalInfoService;

        public PersonalInfoSettingController(IPersonalInfoService personalInfoService)
        {
            _personalInfoService = personalInfoService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index(PersonalInfoFilterDTO filter)
        {
            return View(await _personalInfoService.FilterInfo(filter));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonalInfoCreateDTO info, IFormFile resumeImage, IFormFile avatarImage, IFormFile resumeFile)
        {
            if (ModelState.IsValid)
            {
                if (resumeImage != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(resumeImage.FileName);
                    var result = await resumeImage.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) info.ResumeImage = fileName;
                }

                if (avatarImage != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(avatarImage.FileName);
                    var result = await avatarImage.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) info.AvatarImage = fileName;
                }

                if (resumeFile != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(resumeFile.FileName);
                    var result = await resumeFile.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) info.ResumeFile = fileName;
                }

                await _personalInfoService.AddNewInfo(info);
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", info);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            return View(await _personalInfoService.GetInfoById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonalInfoGetSetDTO info, IFormFile resumeImage, IFormFile avatarImage, IFormFile resumeFile)
        {
            if (ModelState.IsValid)
            {
                if (resumeImage != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(resumeImage.FileName);
                    var result = await resumeImage.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) info.ResumeImage = fileName;
                }

                if (avatarImage != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(avatarImage.FileName);
                    var result = await avatarImage.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) info.AvatarImage = fileName;
                }

                if (resumeFile != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(resumeFile.FileName);
                    var result = await resumeFile.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if (result == UploaderExtension.FileUploadResult.Success) info.ResumeFile = fileName;
                }

                await _personalInfoService.EditInfo(info);
                return RedirectToAction(nameof(Index));
            }

            return View("Edit",info);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _personalInfoService.DeleteInfo(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "PersonalInfoSetting not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "PersonalInfoSetting was deleted!" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetDefault(long id)
        {
            var result = await _personalInfoService.SetDefaultPersonalInfo(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "PersonalInfoSetting not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "PersonalInfoSetting is now default!" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
