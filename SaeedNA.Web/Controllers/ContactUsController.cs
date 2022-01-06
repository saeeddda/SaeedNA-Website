using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Contact;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Web.Controllers
{
    public class ContactUsController : Controller
    {
        #region constractor

        private readonly IPersonalInfoService _personalInfoService;
        private readonly IContactUsService _contactUsService;
        private readonly ISocialMediaService _socialMediaService;

        public ContactUsController(IPersonalInfoService personalInfoService, IContactUsService contactUsService, ISocialMediaService socialMediaService)
        {
            _personalInfoService = personalInfoService;
            _contactUsService = contactUsService;
            _socialMediaService = socialMediaService;
        }

        #endregion

        #region actions

        [HttpGet("contact-us")]
        public async Task<IActionResult> Index()
        {
            ViewBag.PersonalInfo = await _personalInfoService.GetDefaultInfo();
            ViewBag.SocialMedia = await _socialMediaService.FilterSocialMedia(new SocialMediaFilterDTO());

            return View("Index");
        }

        [HttpPost("contact-us"),ValidateAntiForgeryToken,ValidateReCaptcha]
        public async Task<IActionResult> SendMail(string name, string tel, string email, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contact = new ContactUsCreateDTO
                    {
                        Email = email,
                        FullName = name,
                        Mobile = tel,
                        Subject = subject,
                        Text = message
                    };

                    var result = await _contactUsService.AddContactUs(contact);

                    switch (result)
                    {
                        case ServiceResult.Success:
                            return Json(new { status = "success" ,data = "پیام شما با موفقیت ارسال شد."});
                        case ServiceResult.Error:
                            return Json(new { status = "error", data = "مشکلی پیش آمده. پیام شما ارسال نشد." });
                    }
                }
                return Json(new { status = "error",data ="مشکلی پیش آمده. مجدد تلاش کنید." });
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", msg = ex.Message });
            }
        }

        #endregion
    }
}
