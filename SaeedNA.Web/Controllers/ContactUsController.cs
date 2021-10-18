using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SaeedNA.Domain.Models.Email;
using SaeedNA.Framework.Configuration;
using SaeedNA.Framework.Email;
using SaeedNA.Service.Repositories;
using System.Threading.Tasks;
using AspNetCore.ReCaptcha;

namespace SaeedNA.Web.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly SettingManager _settingManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IEmail _email;

        public ContactUsController(
            ISiteSettings siteSettings,
            IEmailSender emailSender,
            IConfiguration configuration,
            IEmail email)
        {
            _settingManager = new SettingManager(siteSettings);
            _emailSender = emailSender;
            _configuration = configuration;
            _email = email;
        }

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags.Split(',');
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;

            //Profile Settings
            ViewBag.FullName = set.FullName;
            ViewBag.Birthday = set.Birthday;
            ViewBag.Mobile = set.Mobile;
            ViewBag.AboutMe = set.AboutMe;
            ViewBag.Slogans = set.Slogans;
            ViewBag.Address = set.Address;
            ViewBag.PhoneNumber = set.PhoneNumber;
            ViewBag.Email = set.Email;
            ViewBag.ResumeImage = set.ResumeImage;
            ViewBag.AvatarImage = set.AvatarImage;
            ViewBag.ResumeFile = set.ResumeFile;

            //Social Icons
            ViewBag.Telegram = set.Telegram;
            ViewBag.Instagram = set.Instagram;
            ViewBag.Twitter = set.Twitter;
            ViewBag.Facebook = set.Facebook;
            ViewBag.Youtube = set.Youtube;
            ViewBag.Linkedin = set.Linkedin;

            ViewData["SiteUrl"] = $"{this.Request.Scheme}://{this.Request.Host.Value}";

            return View("Index", set);
        }

        [HttpPost]
        [ValidateReCaptcha]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMail(string name, string tel, string email, string subject, string message)
        {
            try
            {
                if(!ModelState.IsValid)
                    return RedirectToAction("Index");

                var auth = new EmailAuth()
                {
                    UserName = _configuration.GetSection("MailServer")["Username"],
                    Password = _configuration.GetSection("MailServer")["Password"]
                };

                var provider = new EmailProvider()
                {
                    HostName = _configuration.GetSection("MailServer")["Server"],
                    HostPort = _configuration.GetSection("MailServer")["Port"],
                    HostUsername = _configuration.GetSection("MailServer")["Username"],
                    HostPassword = _configuration.GetSection("MailServer")["Password"],
                    Sender = email
                };

                var content = new EmailContent()
                {
                    ToEmail = _configuration.GetSection("MailServer")["Email"],
                    Subject = subject,
                    Message = $"<b>Sender Name : {name}</b> <br><hr><br> {message}",
                    IsHtml = true
                };

                await _emailSender.SendEmailAsync(auth, provider, content,true);

                var mail = new Email()
                {
                    EmailName = name,
                    EmailPhone = tel,
                    EmailEmail = email,
                    EmailText = message
                };

                _email.AddEmail(mail);

                return Json(new { status = "success" });
            }
            catch(System.Exception ex)
            {
                return Json(new { status = "error", msg = ex.Message });
            }
        }
    }
}
