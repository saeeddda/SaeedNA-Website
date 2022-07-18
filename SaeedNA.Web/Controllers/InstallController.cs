using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaeedNA.Application.Utilities;
using System;
using System.Dynamic;
using System.IO;

namespace SaeedNA.Web.Controllers
{
    public class InstallController : Controller
    {
        /* TODO : 6 Step to install Website
           1- Set connection string
           2- Set Mail Server infos
           3- Set Google recaptcha site key and secret key
           4- Create Settings data
           5- Create Personal Info data
           6- Create Seo Info data
        */

        [HttpGet]
        public IActionResult Index()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            dynamic config = AppSettingsManager.GetAppSettings(env);

            return View("Index",config);
        }

        [HttpPost]
        public IActionResult Index(dynamic config)
        {

            return View("Index",config);
        }
    }
}
