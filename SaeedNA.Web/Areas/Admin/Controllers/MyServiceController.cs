using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Domain.Models.MService;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MyServiceController : Controller
    {
        #region Cotr

        private readonly IMyService _myService;
        private readonly SettingManager _settingManager;

        public MyServiceController(IMyService myService,ISiteSettings siteSettings)
        {
            _myService = myService;
            _settingManager = new SettingManager(siteSettings);
        }

        #endregion

        #region MyService

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var myService = _myService.GetAllMyService();
            return View("Index", myService);
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MyService myService)
        {
            if(ModelState.IsValid)
            {
                _myService.AddMyService(myService);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("myservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Add", myService);
        }

        public IActionResult Edit(string id)
        {
            if(string.IsNullOrEmpty(id))
                return BadRequest();

            var myService = _myService.GetMyServiceById(int.Parse(id));

            if(myService == null)
                return NotFound();

            return PartialView("Edit", myService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MyService myService)
        {
            if(ModelState.IsValid)
            {
                _myService.UpdateMyService(myService);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("myservice", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Edit", myService);
        }

        public JsonResult Delete(string id)
        {
            if(id == "" || string.IsNullOrEmpty(id))
                return Json(new { status = "error", msg = "شناسه نامعتبر است!" });

            var myService = _myService.GetMyServiceById(int.Parse(id));

            if(myService == null)
                return Json(new { status = "error", msg = "تجربه پیدا نشد!" });

            _myService.DeleteMyService(myService.MyServiceId);

            return Json(new { status = "ok", msg = "مورد پاک شد." });
        }

        #endregion
    }
}
