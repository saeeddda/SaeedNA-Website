using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SaeedNA.Service.Interfaces;
using SaeedNA.Web.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SaeedNA.Web.Controllers
{
    public class HomeController : Controller
    {
        #region constractor

        private readonly IPersonalInfoService _personalService;
        private readonly IConfiguration _configuration;

        public HomeController(IPersonalInfoService personalService, IConfiguration configuration)
        {
            _personalService = personalService;
            _configuration = configuration;
        }

        #endregion

        #region actions

        public async Task<IActionResult> Index()
        {
            var IsSiteInstall = bool.Parse(_configuration.GetSection("SiteInstall").Value);
            if (!IsSiteInstall) return RedirectToAction("Index", "Settings",new { area = "Admin" });

            var data = await _personalService.GetDefaultInfo();

            return View("Index", data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
