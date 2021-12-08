using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Domain.Models.MService;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceCounterController : Controller
    {
        #region Cotr

        private readonly ICounterService _serviceCounter;
        private readonly SettingManager _settingManager;

        public ServiceCounterController( ICounterService serviceCounter,ISettingService siteSettings)
        {
            _serviceCounter = serviceCounter;
            _settingManager = new SettingManager(siteSettings);
        }

        #endregion

        #region ServiceCounter

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var serviceCounter = _serviceCounter.GetAllServiceCounter();
            return View("Index", serviceCounter);
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ServiceCounter serviceCounter)
        {
            if(ModelState.IsValid)
            {
                _serviceCounter.AddServiceCounter(serviceCounter);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("counterservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Add", serviceCounter);
        }

        public IActionResult Edit(string id)
        {
            if(string.IsNullOrEmpty(id))
                return BadRequest();

            var serviceCounter = _serviceCounter.GetServiceCounterById(int.Parse(id));

            if(serviceCounter == null)
                return NotFound();

            return PartialView("Edit", serviceCounter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceCounter serviceCounter)
        {
            if(ModelState.IsValid)
            {
                _serviceCounter.UpdateServiceCounter(serviceCounter);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("counterservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Edit", serviceCounter);
        }

        public JsonResult Delete(string id)
        {
            if(id == "" || string.IsNullOrEmpty(id))
                return Json(new { status = "error", msg = "شناسه نامعتبر است!" });

            var serviceCounter = _serviceCounter.GetServiceCounterById(int.Parse(id));

            if(serviceCounter == null)
                return Json(new { status = "error", msg = "تجربه پیدا نشد!" });

            _serviceCounter.DeleteServiceCounter(serviceCounter.ServiceCounterId);

            return Json(new { status = "ok", msg = "مورد پاک شد." });
        }

        #endregion
    }
}
