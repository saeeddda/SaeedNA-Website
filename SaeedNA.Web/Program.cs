
using AspNetCore.ReCaptcha;
using AspNetCore.SEOHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Implementations;
using SaeedNA.Service.Interfaces;
using System.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

#region IoC

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IContactUsService, ContactUsService>();
builder.Services.AddScoped<ICounterService, CounterService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IMSService, MyServiceService>();
builder.Services.AddScoped<IPersonalInfoService, PersonalInfoService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ISeoService, SeoService>();
builder.Services.AddScoped<IGeneralSettingService, GeneralSettingsService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddHttpContextAccessor();

#endregion

#region DBContext

builder.Services.AddDbContext<SaeedNAContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("connectionString")["SqlServer"]);
});

#endregion

#region Data Protection

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + "\\wwwroot\\auth\\"))
    .SetApplicationName("")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(7));

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/log-out";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
});

#endregion

#region Goolge ReCaptcha

builder.Services.AddReCaptcha(builder.Configuration.GetSection("ReCaptcha"));

#endregion

#region Localization

//builder.Services.AddLocalization();

#endregion

#region Seo

builder.Services.AddSeoTags(seoinfo =>
{
    seoinfo.SetSiteInfo(
        siteTitle: "وبسایت شخصی سعید نوری",
        siteTwitterId: "@saeeddda",
        robots: "index, follow"
    );

    seoinfo.SetLocales("fa_IR");
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
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

//var supportedLocales =
//    "en-US,fa-IR"
//        .Split(',')
//        .Select(code => new CultureInfo(code))
//        .ToList();

//app.UseRequestLocalization(new RequestLocalizationOptions
//{
//    SupportedCultures = supportedLocales,
//    SupportedUICultures = supportedLocales,
//});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRobotsTxt(app.Environment.ContentRootPath);
app.UseXMLSitemap(app.Environment.ContentRootPath);

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

app.Run();