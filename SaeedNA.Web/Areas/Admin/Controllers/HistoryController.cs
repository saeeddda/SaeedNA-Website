using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Domain.Models.Resume;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HistoryController : Controller
    {
        #region Cotr

        private readonly IHistory _history;
        private readonly SettingManager _settingManager;

        public HistoryController(IHistory history, ISettingService settingManager)
        {
            _history = history;
            _settingManager = new SettingManager(settingManager);
        }

        #endregion

        #region History

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var history = _history.GetAllHistory();
            return View("Index", history);
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(History history)
        {
            if(ModelState.IsValid)
            {
                _history.AddHistory(history);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("history", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Add", history);
        }

        public IActionResult Edit(string id)
        {
            if(string.IsNullOrEmpty(id))
                return BadRequest();

            var history = _history.GetHistoryById(int.Parse(id));

            if(history == null)
                return NotFound();

            return PartialView("Edit", history);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(History history)
        {
            if(ModelState.IsValid)
            {
                _history.UpdateHistory(history);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("history", "مشکلی در ثبت اطلاعات به وجود آمده!");

            return PartialView("Edit", history);
        }

        public JsonResult Delete(string id)
        {
            if(id == "" || string.IsNullOrEmpty(id))
                return Json(new { status = "error", msg = "شناسه نامعتبر است!" });

            var exp = _history.GetHistoryById(int.Parse(id));

            if(exp == null)
                return Json(new { status = "error", msg = "تجربه پیدا نشد!" });

            _history.DeleteHistory(exp.HistoryId);

            return Json(new { status = "ok", msg = "مورد پاک شد." });
        }

        #endregion
    }
}
