using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Areas.Admin.ViewComponents
{
    public class AdminHeaderViewComponent:ViewComponent
    {
        private readonly IGeneralSettingService _settingService;

        public AdminHeaderViewComponent(IGeneralSettingService settingService)
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
        private readonly IGeneralSettingService _settingService;

        public AdminFooterViewComponent(IGeneralSettingService settingService)
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
        private readonly IPersonalInfoService _personalService;

        public AdminMenuBarViewComponent(IPersonalInfoService personalService)
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