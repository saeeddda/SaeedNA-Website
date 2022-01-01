using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Areas.Admin.ViewComponents
{
    public class AdminHeaderViewComponent:ViewComponent
    {
        private readonly ISettingService _settingService;

        public AdminHeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Settings = await _settingService.GetDefaultSetting();
            return View("AdminHeader");
        }
    }

    public class AdminFooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public AdminFooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Settings = await _settingService.GetDefaultSetting();
            return View("AdminFooter");
        }
    }

    public class AdminMenuBarViewComponent : ViewComponent
    {
        private readonly IPersonalService _personalService;

        public AdminMenuBarViewComponent(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.PersonalInfo = await _personalService.GetDefaultInfo();
            return View("AdminMenuBar");
        }
    }
}