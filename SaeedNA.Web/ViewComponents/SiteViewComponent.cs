using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.ViewComponents
{
    #region Site

    #region site header

    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly IGeneralSettingService _settingService;
        private readonly ISeoService _seoService;
        private readonly IPersonalInfoService _personalService;

        public SiteHeaderViewComponent(IGeneralSettingService settingService, ISeoService seoService, IPersonalInfoService personalService)
        {
            _settingService = settingService;
            _seoService = seoService;
            _personalService = personalService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //ViewBag.Settings = await _settingService.GetDefaultSetting();
            //ViewBag.Seo = await _seoService.GetDefaultSeo();
            //ViewBag.PersonalInfo = await _personalService.GetDefaultInfo();

            return View("SiteHeader");
        }
    }

    #endregion

    #region site footer

    public class SiteFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View("SiteFooter");
        }
    }

    #endregion

    #region site menus

    public class SiteMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View("SiteMenu");
        }
    }

    #endregion

    #endregion

    #region Install

    #region Install Header

    public class InstallHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View("InstallHeader");
        }
    }

    #endregion

    #region Install Footer

    public class InstallFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View("InstallFooter");
        }
    }

    #endregion

    #endregion
}