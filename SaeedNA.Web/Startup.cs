using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaeedNA.Data.Context;
using SaeedNA.Framework.Email;
using SaeedNA.Framework.Identity;
using SaeedNA.Framework.Middlewares;
using SaeedNA.Service.Repositories;
using SaeedNA.Service.Services;
using System;
using System.Globalization;
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

            #region DBContext

            services.AddDbContext<SaeedNAContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("connectionString")["SqlServer"], b => b.MigrationsAssembly("SaeedNA.Web"));
                //options.UseSqlServer("Data Source=.;Initial Catalog=SaeedNADb;Integrated Security=True;", b => b.MigrationsAssembly("SaeedNA.Web"));
            });

            #endregion

            #region Identity

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 0;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                
            })
                .AddEntityFrameworkStores<SaeedNAContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            #endregion

            #region IoC

            services.AddScoped<IPost, PostService>();
            services.AddScoped<ISkill, SkillService>();
            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<IHistory, HistoryService>();
            services.AddScoped<IServiceCounter, ServiceCounterService>();
            services.AddScoped<IMyService, MyServiceService>();
            services.AddScoped<ISiteSettings, SiteSettingsService>();
            services.AddScoped<IEmail, EmailService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IOnlineUser, OnlineUserService>();

            #endregion

            #region Goolge ReCaptcha

            services.AddReCaptcha(Configuration.GetSection("ReCaptcha"));

            #endregion

            #region Localization

            services.AddLocalization();

            #endregion
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
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

            app.UseOnlineUsers();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "admin",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
