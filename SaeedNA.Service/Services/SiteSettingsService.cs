using SaeedNA.Service.Repositories;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.Settings;
using System.Collections.Generic;
using System.Linq;

namespace SaeedNA.Service.Services
{
    public class SiteSettingsService : ISiteSettings
    {
        public readonly SaeedNAContext _context;

        public SiteSettingsService(SaeedNAContext context)
        {
            _context = context;
        }

        public void AddSetting(SiteSettings siteSetting)
        {
            if(siteSetting == null)
                return;

            _context.SiteSettings.Add(siteSetting);
            _context.SaveChanges();
        }

        public ICollection<SiteSettings> GetAllSettings()
        {
            return _context.SiteSettings.ToList();
        }

        public SiteSettings GetSettingByName(string settingName)
        {
            if(settingName == null || string.IsNullOrEmpty(settingName))
                return null;

            var setting = _context.SiteSettings
                .Where(s => s.SettingName == settingName)
                .FirstOrDefault();

            return setting;
        }

        public void UpdateSiteSettings(SiteSettings siteSetting)
        {
            var sett = _context.SiteSettings.
                Where(s => s.SettingId == siteSetting.SettingId && s.SettingName == siteSetting.SettingName);

            if(sett == null)
                return;

            _context.SiteSettings.Update(siteSetting);
            _context.SaveChanges();
        }

        public void SetSetting(string settingName, string settingValue)
        {
            if(settingName == null ||
               string.IsNullOrEmpty(settingName) ||
               settingValue == null ||
               string.IsNullOrEmpty(settingValue))
                return;

            var setting = GetSettingByName(settingName);
            if(setting == null)
            {
                var set = new SiteSettings() {
                    SettingName=settingName,
                    SettingValue=settingValue
                };
                AddSetting(set);
            }
            else
            {
                setting.SettingValue = settingValue;
                UpdateSiteSettings(setting);
            }
        }

        public string GetSetting(string settingName)
        {
            if(settingName == null ||
               string.IsNullOrEmpty(settingName))
                return null;

            var setting = GetSettingByName(settingName).SettingValue;

            if(setting == null)
                setting = "";

            return setting;
        }
    }
}
