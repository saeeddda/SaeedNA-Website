using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Areas.Admin.ViewComponents
{
    public class AdminHeaderViewComponent:ViewComponent
    {
        private readonly IGeneralSettingService _generalSettingService;

        public AdminHeaderViewComponent(IGeneralSettingService generalSettingService)
        {
            _generalSettingService = generalSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _generalSettingService.GetDefaultSetting();
            return View("AdminHeader", data);
        }
    }

    public class AdminFooterViewComponent : ViewComponent
    {
        private readonly IGeneralSettingService _generalSettingService;

        public AdminFooterViewComponent(IGeneralSettingService generalSettingService)
        {
            _generalSettingService = generalSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _generalSettingService.GetDefaultSetting();
            return View("AdminFooter",data);
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
            var data = await _personalService.GetDefaultInfo();
            return View("AdminMenuBar",data);
        }
    }
}