using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Domain.Models.SPost;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        #region Ctor

        private readonly ICategoryService _category;
        private readonly SettingManager _settingManager;

        public CategoryController(ICategoryService category,ISettingService siteSettings)
        {
            _category = category;
            _settingManager = new SettingManager(siteSettings);
        }

        #endregion

        #region Category Actions

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var cat = _category.GetAllCategory();
            return View("Index", cat);
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category category)
        {
            if(ModelState.IsValid)
            {
                _category.AddCategory(category);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("category", "یک یا چند فیلد تکمیل نشده است!");

            return PartialView("Add", category);
        }

        public IActionResult Edit(string id)
        {
            if(string.IsNullOrEmpty(id))
                return BadRequest();

            var cat = _category.GetCategoryById(int.Parse(id));

            if(cat == null)
                return NotFound();

            return PartialView("Edit", cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                _category.UpdateCategory(category);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("category", "یک یا چند فیلد تکمیل نشده است!");

            return PartialView("Edit",category);
        }

        public JsonResult Delete(string id)
        {
            if(string.IsNullOrEmpty(id))
                return Json(new { status = "error", msg = "ID not valid!" });

            var cat = _category.GetCategoryById(int.Parse(id));

            if(cat == null)
                return Json(new { status = "error", msg = "Category not found!" });

            _category.DeleteCategory(int.Parse(id));

            return Json(new { status = "ok", msg = "Category deleted" });
        }

        #endregion
    }
}
