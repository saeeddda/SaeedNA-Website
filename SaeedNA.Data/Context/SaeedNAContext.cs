using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaeedNA.Domain.Models.SPost;
using SaeedNA.Domain.Models.Resume;
using SaeedNA.Domain.Models.MService;
using SaeedNA.Domain.Models.Settings;
using SaeedNA.Domain.Models.Email;

namespace SaeedNA.Data.Context
{
    public class SaeedNAContext : IdentityDbContext
    {
        public SaeedNAContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SiteSettings>().HasData(
                //Site Settings
                new SiteSettings()
                {
                    SettingId = 101,
                    SettingName = "SiteLogo",
                    SettingValue = "/uploads/img/SiteLogo.png"
                },
                new SiteSettings()
                {
                    SettingId = 102,
                    SettingName = "SiteFavIcon",
                    SettingValue = "/uploads/img/SiteLogo.png"
                },
                new SiteSettings()
                {
                    SettingId = 103,
                    SettingName = "SiteUrl",
                    SettingValue = "https://saeedna.ir/"
                },
                new SiteSettings()
                {
                    SettingId = 104,
                    SettingName = "SiteTitle",
                    SettingValue = "سایت شخصی سعید نوری"
                },
                new SiteSettings()
                {
                    SettingId = 105,
                    SettingName = "SiteMode",
                    SettingValue = "dark"
                },
                new SiteSettings()
                {
                    SettingId = 106,
                    SettingName = "SiteColor",
                    SettingValue = "blue"
                },
                new SiteSettings()
                {
                    SettingId = 107,
                    SettingName = "MetaTags",
                    SettingValue = "شخصی,سایت,رزومه,وردپرس,نوری,wordpress,طراحی"
                },
                new SiteSettings()
                {
                    SettingId = 108,
                    SettingName = "MetaDescription",
                    SettingValue = "سایت شخصی سعید نوری. انجام طراحی سایت و گرافیک"
                },
                new SiteSettings()
                {
                    SettingId = 109,
                    SettingName = "GoogleAnalytics",
                    SettingValue = ""
                },
                new SiteSettings()
                {
                    SettingId = 110,
                    SettingName = "MainMenu",
                    SettingValue = "true"
                },
                new SiteSettings()
                {
                    SettingId = 111,
                    SettingName = "PortfolioMenu",
                    SettingValue = "true"
                },
                new SiteSettings()
                {
                    SettingId = 112,
                    SettingName = "BlogMenu",
                    SettingValue = "true"
                },
                new SiteSettings()
                {
                    SettingId = 113,
                    SettingName = "ContactMeMenu",
                    SettingValue = "true"
                },
                new SiteSettings()
                {
                    SettingId = 114,
                    SettingName = "AboutMeMenu",
                    SettingValue = "true"
                },
                //Site Profile
                new SiteSettings()
                {
                    SettingId = 115,
                    SettingName = "FullName",
                    SettingValue = "سعید نوری"
                },
                new SiteSettings()
                {
                    SettingId = 116,
                    SettingName = "Birthday",
                    SettingValue = "1375/04/20"
                },
                new SiteSettings()
                {
                    SettingId = 117,
                    SettingName = "Mobile",
                    SettingValue = "09101650281"
                },
                new SiteSettings()
                {
                    SettingId = 118,
                    SettingName = "AboutMe",
                    SettingValue = "سایت شخصی سعید نوری"
                },
                new SiteSettings()
                {
                    SettingId = 119,
                    SettingName = "Slogans",
                    SettingValue = "طراح سایت و اپلیکیشن"
                },
                new SiteSettings()
                {
                    SettingId = 120,
                    SettingName = "ResumeImage",
                    SettingValue = "/uploads/img/2.jpg"
                },
                new SiteSettings()
                {
                    SettingId = 121,
                    SettingName = "AvatarImage",
                    SettingValue = "/uploads/img/img-mobile.jpg"
                },
                new SiteSettings()
                {
                    SettingId = 122,
                    SettingName = "ResumeFile",
                    SettingValue = "/uploads/img/resume.pdf"
                },
                new SiteSettings()
                {
                    SettingId = 123,
                    SettingName = "Address",
                    SettingValue = "ایران، تهران"
                },
                new SiteSettings()
                {
                    SettingId = 124,
                    SettingName = "PhoneNumber",
                    SettingValue = "09101650281"
                },
                new SiteSettings()
                {
                    SettingId = 125,
                    SettingName = "Email",
                    SettingValue = "i@saeedna.ir"
                },
                //Social Icons
                new SiteSettings()
                {
                    SettingId = 126,
                    SettingName = "Telegram",
                    SettingValue = "https://t.me/saeeddda_main"
                },
                new SiteSettings()
                {
                    SettingId = 127,
                    SettingName = "Instagram",
                    SettingValue = "https://instagram.com/saeeddda_main"
                },
                new SiteSettings()
                {
                    SettingId = 128,
                    SettingName = "Twitter",
                    SettingValue = "https://twitter.com/saeeddda"
                },
                new SiteSettings()
                {
                    SettingId = 129,
                    SettingName = "Facebook",
                    SettingValue = "#"
                },
                new SiteSettings()
                {
                    SettingId = 130,
                    SettingName = "Youtube",
                    SettingValue = "#"
                },
                new SiteSettings()
                {
                    SettingId = 131,
                    SettingName = "Linkedin",
                    SettingValue = "#"
                },
                new SiteSettings()
                {
                    SettingId = 132,
                    SettingName = "Age",
                    SettingValue = "25"
                },
                new SiteSettings()
                {
                    SettingId = 133,
                    SettingName = "Language",
                    SettingValue = "فارسی، ترکی، انگلیسی"
                },
                new SiteSettings()
                {
                    SettingId = 134,
                    SettingName = "Nationality",
                    SettingValue = "ایران"
                }
            );

            builder.Entity<MyService>().HasData(
                new MyService()
                {
                    MyServiceId = 101,
                    MyServiceTitle = "طراحی وب",
                    MyServiceText = "طراحی سایت طبق نیاز شما"
                },
                new MyService()
                {
                    MyServiceId = 102,
                    MyServiceTitle = "طراحی اپلیکیشن",
                    MyServiceText = "طراحی اپلیکیشن موبایل برای انواع نیاز ها"
                },
                new MyService()
                {
                    MyServiceId = 103,
                    MyServiceTitle = "طراحی گرافیکی",
                    MyServiceText = "طراحی انواع کارت ویزیت و تراکت"
                },
                new MyService()
                {
                    MyServiceId = 104,
                    MyServiceTitle = "پشتیبانی",
                    MyServiceText = "انجام پشتیبانی بر روی پروژه های شما"
                }
            );

            builder.Entity<ServiceCounter>().HasData(
                new ServiceCounter()
                {
                    ServiceCounterId = 101,
                    ServiceCounterTitle = "انجام شده",
                    ServiceCounterCount = "80"
                },
                new ServiceCounter()
                {
                    ServiceCounterId = 102,
                    ServiceCounterTitle = "فنجان قهوه",
                    ServiceCounterCount = "850"
                },
                new ServiceCounter()
                {
                    ServiceCounterId = 103,
                    ServiceCounterTitle = "مشتریان",
                    ServiceCounterCount = "56"
                },
                new ServiceCounter()
                {
                    ServiceCounterId = 104,
                    ServiceCounterTitle = "ساعت کد زدن",
                    ServiceCounterCount = "4000"
                }
            ) ;

            builder.Entity<Skill>().HasData(
                new Skill()
                {
                    SkillId = 101,
                    SkillTitle="C#",
                    SkillProgress="80"
                },
                new Skill()
                {
                    SkillId = 102,
                    SkillTitle = "ASP.Net Core",
                    SkillProgress = "60"
                },
                new Skill()
                {
                    SkillId = 103,
                    SkillTitle = "Wordpress",
                    SkillProgress = "85"
                },
                new Skill()
                {
                    SkillId = 104,
                    SkillTitle = "HTML, Css",
                    SkillProgress = "90"
                },
                new Skill()
                {
                    SkillId = 105,
                    SkillTitle = "PhotoShop",
                    SkillProgress = "80"
                },
                new Skill()
                {
                    SkillId = 106,
                    SkillTitle = "Seo",
                    SkillProgress = "60"
                }
            ) ;

            base.OnModelCreating(builder);
        }

        public DbSet<ServiceCounter> ServiceCounters { get; set; }
        public DbSet<MyService> MyServices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}
