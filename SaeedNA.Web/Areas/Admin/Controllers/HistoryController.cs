using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Resume;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HistoryController : Controller
    {
        #region Cotr

        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        #endregion

        #region History

        public async Task<IActionResult> Index(HistoryFilterDTO filter)
        {
            return View("Index",await _historyService.FilterHistory(filter) );
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(HistoryCreateDTO history)
        {
            if(ModelState.IsValid)
            {
                await _historyService.AddNewHistory(history);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("history", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Add", history);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var history = await _historyService.GetHistoryById(id);

            if(history == null) return NotFound();

            return PartialView("Edit", history);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HistoryEditDTO history)
        {
            if(ModelState.IsValid)
            {
                await _historyService.EditHistory(history);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("history", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Edit", history);
        }

        public async Task<JsonResult> Delete(long id)
        {
            var result = await _historyService.DeleteHistory(id);

            switch(result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "History not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "History deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
