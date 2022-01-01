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
    public class PostController : Controller
    {
        #region Ctor

        private readonly ICategoryService _category;
        private readonly IPostService _post;

        public PostController(ICategoryService category, IPostService post)
        {
            _category = category;
            _post = post;
        }

        #endregion

        #region Post Actions

        public async Task<IActionResult> Index(PostFilterDTO filter)
        {
            return View("Index", await _post.FilterPost(filter));
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
        public async Task<IActionResult> Add(PostCreateDTO post, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                    var result = await file.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if(result == UploaderExtension.FileUploadResult.Success) post.Image = fileName;
                }

                await _post.AddNewPost(post);
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

            return View("Add", post);
        }

        public async Task<IActionResult> Edit(long id)
        {

            var post = await _post.GetPostForEdit(id);

            if(post == null) return NotFound();

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

            return View("Edit", post);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostEditDTO post, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                    var result = await file.UploadToServer(fileName, PathExtension.UploadPathServer, null, null);
                    if(result == UploaderExtension.FileUploadResult.Success) post.Image = fileName;
                }

                await _post.EditPost(post);
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

            return View("Edit", post);
        }

        public async Task<JsonResult> Delete(long id)
        {
            var result = await _post.DeletePost(id);

            switch(result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "Post not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "Post deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion 
    }
}
