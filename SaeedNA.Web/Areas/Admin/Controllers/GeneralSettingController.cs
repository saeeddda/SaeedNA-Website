using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SaeedNA.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class GeneralSettingController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            return View();
        }
    }
}
