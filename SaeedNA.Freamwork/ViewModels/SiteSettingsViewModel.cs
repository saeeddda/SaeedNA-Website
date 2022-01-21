using SaeedNA.Data.DTOs.Site;

namespace SaeedNA.Application.ViewModels
{
    public class SiteSettingsViewModel
    {
        public SettingGetSetDTO  Setting { get; set; }
        public PersonalInfoGetSetDTO PersonalInfo { get; set; }
        public SeoGetSetDTO Seo { get; set; }
    }
}
