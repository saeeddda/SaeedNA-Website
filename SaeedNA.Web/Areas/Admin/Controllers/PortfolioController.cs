using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SaeedNA.Application.Utilities;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PortfolioController : Controller
    {
        #region Ctor

        private readonly ICategoryService _category;
        private readonly IPortfolioService _portfolio;

        public PortfolioController(ICategoryService category, IPortfolioService portfolio)
        {
            _category = category;
            _portfolio = portfolio;
        }

        #endregion

        #region Portfolio Actions

        public async Task<IActionResult> Index(PortfolioFilterDTO filter)
        {
            return View("Index", await _portfolio.FilterPortfolio(filter));
        }

        public async Task<IActionResult> Add()
        {

            var cat = await _category.FilterCategory(new CategoryFilterDTO() { });
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat.Categories)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Categories = listitem;

            return View("Add");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PortfolioCreateDTO portfolio, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                    var result = await file.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if(result == UploaderExtension.FileUploadResult.Success) portfolio.Image = fileName;
                }

                await _portfolio.AddNewPortfolio(portfolio);
                return RedirectToAction("Index", new { area = "Admin" });
            }

            ModelState.Clear();
            ModelState.AddModelError("blog", "مشکلی پیش آمده!");

            var cat = await _category.FilterCategory(new CategoryFilterDTO() { });
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat.Categories)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Categories = listitem;

            return View("Add", portfolio);
        }

        public async Task<IActionResult> Edit(long id)
        {

            var portfolio = await _portfolio.GetPortfolioForEdit(id);

            if(portfolio == null) return NotFound();

            var cat = await _category.FilterCategory(new CategoryFilterDTO() { });
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat.Categories)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.Categories = listitem;

            return View("Edit", portfolio);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PortfolioEditDTO portfolio, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                    var result = await file.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if(result == UploaderExtension.FileUploadResult.Success) portfolio.Image = fileName;
                }

                await _portfolio.EditPortfolio(portfolio);
                return RedirectToAction("Index", new { area = "Admin" });

            }

            ModelState.Clear();
            ModelState.AddModelError("blog", "مشکلی پیش آمده!");

            var cat = await _category.FilterCategory(new CategoryFilterDTO() { });
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat.Categories)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Categories = listitem;

            return View("Edit", portfolio);
        }

        public async Task<JsonResult> Delete(long id)
        {
            var result = await _portfolio.DeletePortfolio(id);

            switch(result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "Portfolio not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "Portfolio deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion 
    }
}
