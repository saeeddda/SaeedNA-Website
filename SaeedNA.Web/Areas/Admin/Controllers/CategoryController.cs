﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        #region Ctor

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Category Actions

        [HttpGet]
        public async Task<IActionResult> Index(CategoryFilterDTO filter)
        {
            //filter.IsDelete = false;
            return View("Index", await _categoryService.FilterCategory(filter));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryCreateDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddNewCategory(category);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("Name", "یک یا چند فیلد تکمیل نشده است!");

            return PartialView("Add", category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var cat = await _categoryService.EditCategoryById(id);

            if (cat == null) return NotFound();

            return PartialView("Edit", cat);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.EditCategory(category);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("Name", "یک یا چند فیلد تکمیل نشده است!");

            return PartialView("Edit", category);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(long id)
        {
            var result = await _categoryService.DeleteCategory(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "Category not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "Category deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
