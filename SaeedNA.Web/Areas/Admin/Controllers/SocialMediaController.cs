using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class SocialMediaController : Controller
    {
        #region Ctor

        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index(SocialMediaFilterDTO filter)
        {
            return View("Index", await _socialMediaService.FilterSocialMedia(filter));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SocialMediaCreateDTO socialMedia)
        {
            if (ModelState.IsValid)
            {
                await _socialMediaService.AddNewSocialMedia(socialMedia);
                return RedirectToAction(nameof(Index));
            }

            return PartialView(socialMedia);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var data = await _socialMediaService.GetSocialMediaForEdit(id);

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SocialMediaEditDTO socialMedia)
        {
            if (ModelState.IsValid)
            {
                await _socialMediaService.EditSocialMedia(socialMedia);
                return RedirectToAction(nameof(Index));
            }

            return PartialView(socialMedia);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _socialMediaService.DeleteSocialMedia(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "SocialMedia not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "SocialMedia deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
