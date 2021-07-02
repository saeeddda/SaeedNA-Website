using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Service.ViewModels
{
    public class SiteSettingViewModel
    {
        //Site Settings
        [Display(Name = "لوگو سایت")]
        public string SiteLogo { get; set; }
        [Display(Name = "فاو آیکون")]
        public string SiteFavIcon { get; set; }
        [Display(Name = "آدرس سایت")]
        public string SiteUrl { get; set; }
        [Display(Name = "عنوان سایت")]
        public string SiteTitle { get; set; }
        [Display(Name = "حالت سایت")]
        public string SiteMode { get; set; }
        [Display(Name = "رنگ سایت")]
        public string SiteColor { get; set; }
        [Display(Name = "تگ های سئو")]
        public string MetaTags { get; set; }
        [Display(Name = "توضیحات سئو")]
        public string MetaDescription { get; set; }
        [Display(Name = "کد گوگل آنالایتیکس")]
        public string GoogleAnalytics { get; set; }
        [Display(Name = "منوی اصلی")]
        public bool MainMenu { get; set; }
        [Display(Name = "منوی نمونه کار")]
        public bool PortfolioMenu { get; set; }
        [Display(Name = "منوی مقالات")]
        public bool BlogMenu { get; set; }
        [Display(Name = "منوی تماس با من")]
        public bool ContactMeMenu { get; set; }
        [Display(Name = "منوی درباره من")]
        public bool AboutMeMenu { get; set; }

        //Site Profile
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string Birthday { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "درباره من")]
        public string AboutMe { get; set; }
        [Display(Name = "شعارها")]
        public string Slogans { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "سن")]
        public string Age { get; set; }
        [Display(Name = "زبان")]
        public string Language { get; set; }
        [Display(Name = "ملیت")]
        public string Nationality { get; set; }
        [Display(Name = "تصویر رزومه")]
        public string ResumeImage { get; set; }
        [Display(Name = "تصویر آواتار")]
        public string AvatarImage { get; set; }
        [Display(Name = "فایل رزومه")]
        public string ResumeFile { get; set; }

        //Social Icons
        [Display(Name = "آدرس تلگرام")]
        public string Telegram { get; set; }
        [Display(Name = "آدرس اینستاگرام")]
        public string Instagram { get; set; }
        [Display(Name = "آدرس توئیتر")]
        public string Twitter { get; set; }
        [Display(Name = "آدرس فیس بوک")]
        public string Facebook { get; set; }
        [Display(Name = "آدرس یوتویب")]
        public string Youtube { get; set; }
        [Display(Name = "آدرس لینکدین")]
        public string Linkedin { get; set; }
    }
}
