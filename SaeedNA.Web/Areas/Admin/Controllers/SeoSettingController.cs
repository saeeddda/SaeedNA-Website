using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.Interfaces;
using SaeedNA.Data.DTOs.Site;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;

namespace SaeedNA.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class SeoSettingController : Controller
    {
        #region Ctor

        private readonly ISeoService _seoService;

        public SeoSettingController(ISeoService seoService)
        {
            _seoService = seoService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index(SeoFilterDTO filter)
        {
            return View( await _seoService.FilterSeo(filter));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeoCreateDTO seo)
        {
            if (ModelState.IsValid)
            {
                await _seoService.AddNewSeo(seo);
                return RedirectToAction(nameof(Index));
            }

            return PartialView("Add",seo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            return PartialView("Edit", await _seoService.GetSeoById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeoGetSetDTO seo)
        {
            if (ModelState.IsValid)
            {
                await _seoService.EditSeo(seo);
                return RedirectToAction(nameof(Index));
            }

            return PartialView("Edit",seo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _seoService.DeleteSeo(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "SeoSetting not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "SeoSetting deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetDefault(long id)
        {
            var result = await _seoService.SetDefaultSeo(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "SeoSetting not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "SeoSetting is now default!" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
