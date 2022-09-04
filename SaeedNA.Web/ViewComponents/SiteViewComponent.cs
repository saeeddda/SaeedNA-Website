using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Application.ViewModels;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.ViewComponents
{
    #region Site

    #region site header

    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly IGeneralSettingService _generalSettingService;
        private readonly ISeoService _seoService;
        private readonly IPersonalInfoService _personalService;

        public SiteHeaderViewComponent(IGeneralSettingService generalSettingService, ISeoService seoService, IPersonalInfoService personalService)
        {
            _generalSettingService = generalSettingService;
            _seoService = seoService;
            _personalService = personalService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var general = await _generalSettingService.GetDefaultSetting();
            var seo = await _seoService.GetDefaultSeo();
            var personalInfo = await _personalService.GetDefaultInfo();

            var data = new SiteSettingsViewModel()
            {
                Settings = general,
                Seos = seo,
                PersonalInfos = personalInfo
            };

            return View("SiteHeader", data);
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