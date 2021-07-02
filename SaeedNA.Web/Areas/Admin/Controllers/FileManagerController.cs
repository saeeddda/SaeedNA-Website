using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    public class FileManagerController : Controller
    {
        //#region FileUploader

        //[HttpPost]
        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    try
        //    {
        //        if(file == null || file.Length == 0)
        //            return BadRequest();

        //        if(file.Length > 20000000)
        //            return Content("File size to long!");

        //        var ext = Path.GetExtension(file.FileName);

        //        if(!AllowedFileTypes().Contains(ext))
        //            return Content("File type not support!");

        //        var randomName = Path.GetRandomFileName() + file.FileName;
        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", randomName);

        //        using(var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        var types = GetMimeTypes();
        //        var fileType = types[ext];

        //        return View();
        //    }
        //    catch(Exception)
        //    {
        //        return BadRequest();
        //    }
        //}


        //public async Task<JsonResult> AjaxUpload(IFormFile file)
        //{
        //    try
        //    {
        //        if(file == null || file.Length == 0)
        //            return Json(new { status = "error", message = "Bad Request!" });

        //        if(file.Length > 20000000)
        //            return Json(new { status = "error", message = "File size to long!, file size must be 20mb." });

        //        var ext = Path.GetExtension(file.FileName);

        //        if(!AllowedFileTypes().Contains(ext))
        //            return Json(new { status = "error", message = "File type not support!" });

        //        var randomName = Path.GetRandomFileName() + file.FileName;
        //        var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","uploads");

        //        if(!Directory.Exists(directory))
        //            Directory.CreateDirectory(directory);

        //        var path = Path.Combine(directory, randomName);

        //        using(var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        var fileUrl = Url.Content("~/uploads/" + randomName);

        //        var types = GetMimeTypes();
        //        var fileType = types[ext];

        //        return Json(new { status = "success", url = fileUrl });
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(new { status = "error", message = ex.Message });
        //    }
        //}

        //private static string GetContentType(string filePath)
        //{
        //    var types = GetMimeTypes();
        //    var ext = Path.GetExtension(filePath).ToLowerInvariant();
        //    return types[ext];
        //}

        //private static Dictionary<string, string> GetMimeTypes()
        //{
        //    return new Dictionary<string, string>
        //    {
        //        {".txt", "text/plain"},
        //        {".pdf", "application/pdf"},
        //        {".doc", "application/vnd.ms-word"},
        //        {".docx", "application/vnd.ms-word"},
        //        {".xls", "application/vnd.ms-excel"},
        //        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        //        {".png", "image/png"},
        //        {".jpg", "image/jpeg"},
        //        {".jpeg", "image/jpeg"},
        //        {".gif", "image/gif"},
        //        {".csv", "text/csv"},
        //        {".ico","image/x-icon" }
        //    };
        //}

        //private List<string> AllowedFileTypes()
        //{
        //    var type = new List<string>();
        //    type.Add(".txt");
        //    type.Add(".pdf");
        //    type.Add(".doc");
        //    type.Add(".docx");
        //    type.Add(".xls");
        //    type.Add(".xlsx");
        //    type.Add(".png");
        //    type.Add(".jpg");
        //    type.Add(".jpeg");
        //    type.Add(".gif");
        //    type.Add(".csv");
        //    type.Add(".ico");
        //    return type;
        //}

        //#endregion
    }
}
