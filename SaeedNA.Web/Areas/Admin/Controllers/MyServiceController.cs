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
    public class MyServiceController : Controller
    {
        #region Cotr

        private readonly IMSService _myService;

        public MyServiceController(IMSService myService)
        {
            _myService = myService;
        }

        #endregion

        #region MyService

        [HttpGet]
        public async Task<IActionResult> Index(MyServiceFilterDTO filter)
        {
            return View("Index",await _myService.FilterService(filter));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MyServiceCreateDTO myService)
        {
            if(ModelState.IsValid)
            {
                await _myService.AddNewService(myService);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("myservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Add", myService);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var myService = await _myService.GetMyServiceById(id);

            if(myService == null) return NotFound();

            return PartialView("Edit", myService);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MyServiceEditDTO myService)
        {
            if(ModelState.IsValid)
            {
                await _myService.EditService(myService);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("myservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Edit", myService);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(long id)
        {
            var result = await _myService.DeleteService(id);

            switch(result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "Service not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "Service deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
