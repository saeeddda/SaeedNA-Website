using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Contact;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EmailController : Controller
    {
        #region Ctor

        private readonly IContactUsService _contactUsService;

        public EmailController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        #endregion

        #region Email Actions

        [HttpGet]
        public async Task<IActionResult> Index(ContactUsFilterDTO filter)
        {
            return View("Index",await _contactUsService.FilterContactUs(filter));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var email = await _contactUsService.GetContactUs(id);
            if(email == null) return NotFound();

            return PartialView("Detail",email);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsReadMessage(long id)
        {
            var result = await _contactUsService.MarkAsRead(id);

            switch (result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "ContactUs not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "ContactUs deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
