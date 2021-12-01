using SaeedNA.Domain.Models.Settings;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface ISiteSettings
    {
        SiteSettings GetSettingByName(string settingName);
        string GetSetting(string settingName);
        void SetSetting(string settingName, string settingValue);
        ICollection<SiteSettings> GetAllSettings();
        void AddSetting(SiteSettings siteSetting);
        void UpdateSiteSettings(SiteSettings siteSetting);
    }
}
