using Microsoft.AspNetCore.Mvc;
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

        public HomeController(IPersonalInfoService personalService)
        {
            _personalService = personalService;
        }

        #endregion

        #region actions

        public async Task<IActionResult> Index()
        {
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
