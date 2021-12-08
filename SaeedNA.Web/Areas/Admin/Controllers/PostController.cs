using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaeedNA.Domain.Models.SPost;
using SaeedNA.Framework.Configuration;
using SaeedNA.Framework.Utilities;
using SaeedNA.Service.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        #region Ctor

        private readonly ICategoryService _category;
        private readonly IPostService _post;
        private Uploader _fileUploader;
        private readonly SettingManager _settingManager;

        public PostController(ICategoryService category, IPostService post,ISettingService siteSettings)
        {
            _category = category;
            _post = post;
            _settingManager = new SettingManager(siteSettings);
        }

        #endregion

        #region Post Actions

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var post = _post.GetAllWithCategory();
            return View("Index", post);
        }

        public IActionResult Add()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;
            
            var cat = _category.GetAllCategory();
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.CategoryId.ToString()
                });
            }

            ViewBag.Categories = listitem;

            return View("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Post post, IFormFile file)
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    _fileUploader = new Uploader();
                    string uploadFolder = "Uploads";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadFolder);

                    string fileRandomName = await _fileUploader.FileUpload(file, path);
                    string fileUrl = Url.Content("~/" + uploadFolder + "/" + fileRandomName);
                    post.PostFile = fileUrl;
                }

                post.PostCreateDate = DateTime.Now.ToShamsi();
                // var cate = _category.GetCategoryById(post.CategoryId);
                // post.Category = cate;
                post.PostVisit = 0;
                _post.AddPost(post);
                return RedirectToAction("Index", new { area = "Admin" });
            }

            ModelState.Clear();
            ModelState.AddModelError("blog", "مشکلی پیش آمده!");

            var cat = _category.GetAllCategory();
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.CategoryId.ToString()
                });
            }

            ViewBag.Categories = listitem;

            return View("Add", post);
        }

        public IActionResult Edit(string id)
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            if(string.IsNullOrEmpty(id))
                return BadRequest();

            var post = _post.GetPostById(int.Parse(id));

            if(post == null)
                return NotFound();

            var cat = _category.GetAllCategory();
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.CategoryId.ToString()
                });
            }
            ViewData["SiteUrl"] = $"{this.Request.Scheme}://{this.Request.Host.Value}";
            ViewBag.Categories = listitem;

            return View("Edit", post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post post, IFormFile file)
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    _fileUploader = new Uploader();
                    string uploadFolder = "Uploads";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadFolder);

                    string fileRandomName = await _fileUploader.FileUpload(file, path);
                    string fileUrl = Url.Content("~/" + uploadFolder +"/"+ fileRandomName);
                    post.PostFile = fileUrl;
                }

                _post.UpdatePost(post);
                return RedirectToAction("Index", new { area = "Admin" });
            }

            ModelState.Clear();
            ModelState.AddModelError("blog", "مشکلی پیش آمده!");

            var cat = _category.GetAllCategory();
            List<SelectListItem> listitem = new List<SelectListItem>();

            foreach(var item in cat)
            {
                listitem.Add(new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.CategoryId.ToString()
                });
            }

            ViewBag.Categories = listitem;

            return View("Edit", post);
        }

        public JsonResult Delete(string id)
        {
            if(string.IsNullOrEmpty(id))
                return Json(new { status = "error", msg = "ID not valid!" });

            var post = _post.GetPostById(int.Parse(id));

            if(post == null)
                return Json(new { status = "error", msg = "Post not found!" });

            //System.IO.File.Delete(post.PostFile);

            _post.DeletePost(int.Parse(id));

            return Json(new { status = "ok", msg = "Post deleted" });
        }

        #endregion 
    }
}
