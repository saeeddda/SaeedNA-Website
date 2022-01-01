using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.MService;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceCounterController : Controller
    {
        #region Cotr

        private readonly ICounterService _counterService;

        public ServiceCounterController(ICounterService counterService)
        {
            _counterService = counterService;
        }

        #endregion

        #region ServiceCounter

        public async Task<IActionResult> Index(CounterFilterDTO filter)
        {
            return View("Index", await _counterService.FilterCounter(filter));
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CounterCreateDTO counter)
        {
            if(ModelState.IsValid)
            {
                await _counterService.AddNewCounter(counter);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("counterservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Add", counter);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var serviceCounter = await _counterService.GetCounterById(id);

            if(serviceCounter == null) return NotFound();

            return PartialView("Edit", serviceCounter);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CounterEditDTO counter)
        {
            if(ModelState.IsValid)
            {
                await _counterService.EditCounter(counter);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("counterservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Edit", counter);
        }

        public async Task<JsonResult> Delete(long id)
        {
            var result = await _counterService.DeleteCounter(id);

            switch(result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "Counter not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "Counter deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
