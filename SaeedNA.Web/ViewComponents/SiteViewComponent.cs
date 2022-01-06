using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.ViewComponents
{
    #region site header

    public class SiteHeaderViewComponent:ViewComponent
    {
        private readonly ISiteSettingService _settingService;
        private readonly ISeoService _seoService;
        private readonly IPersonalInfoService _personalService;

        public SiteHeaderViewComponent(ISiteSettingService settingService, ISeoService seoService, IPersonalInfoService personalService)
        {
            _settingService = settingService;
            _seoService = seoService;
            _personalService = personalService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Settings = await _settingService.GetDefaultSetting();
            ViewBag.Seo = await _seoService.GetDefaultSeo();
            ViewBag.PersonalInfo = await _personalService.GetDefaultInfo();

            return View("SiteHeader");
        }
    }

    #endregion

    #region site footer

    public class SiteFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteFooter");
        }
    }

    #endregion

    #region site menus

    public class SiteMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteMenu");
        }
    }

    #endregion
}