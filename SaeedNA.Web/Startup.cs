using AspNetCore.ReCaptcha;
using AspNetCore.SEOHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Implementations;
using SaeedNA.Service.Interfaces;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SaeedNA.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region IoC

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<ICounterService, CounterService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IMSService, MyServiceService>();
            services.AddScoped<IPersonalInfoService, PersonalInfoService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ISeoService, SeoService>();
            services.AddScoped<ISiteSettingService, SiteSettingsService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddHttpContextAccessor();

            #endregion

            #region DBContext

            services.AddDbContext<SaeedNAContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("connectionString")["SqlServer"]);
            });

            #endregion

            #region Data Protection

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + "\\wwwroot\\auth\\"))
                .SetApplicationName("")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(7));

            #endregion

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme= CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/log-out";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            #endregion

            #region Goolge ReCaptcha

            services.AddReCaptcha(Configuration.GetSection("ReCaptcha"));

            #endregion

            #region Localization

            services.AddLocalization();

            #endregion

            #region Seo

            services.AddSeoTags(seoinfo =>
            {
                seoinfo.SetSiteInfo(
                    siteTitle: "وبسایت شخصی سعید نوری",
                    siteTwitterId: "@saeeddda",
                    robots: "index, follow"
                );

                seoinfo.SetLocales("fa_IR");
            });

            #endregion
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var supportedLocales =
                "en-US,fa-IR"
                    .Split(',')
                    .Select(code => new CultureInfo(code))
                    .ToList();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                SupportedCultures = supportedLocales,
                SupportedUICultures = supportedLocales,
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRobotsTxt(env.ContentRootPath);
            app.UseXMLSitemap(env.ContentRootPath);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
