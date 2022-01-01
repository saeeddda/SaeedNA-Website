using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(ContactUsFilterDTO filter)
        {
            return View("Index",await _contactUsService.FilterContactUs(filter));
        }

        public async Task<IActionResult> Detail(long id)
        {
            var email = await _contactUsService.GetContactUs(id);
            if(email == null) return NotFound();

            return PartialView("Detail",email);
        }

        #endregion
    }
}
