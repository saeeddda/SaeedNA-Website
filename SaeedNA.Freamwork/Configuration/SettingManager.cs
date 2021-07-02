using SaeedNA.Service.Repositories;
using SaeedNA.Service.ViewModels;

namespace SaeedNA.Framework.Configuration
{
    public class SettingManager
    {
        private readonly ISiteSettings _siteSettings;

        public SettingManager(ISiteSettings siteSettings)
        {
            _siteSettings = siteSettings;
        }

        public SiteSettingViewModel GetAllSettings()
        {
            var setting = new SiteSettingViewModel()
            {
                //Site Settings
                SiteLogo = _siteSettings.GetSetting("SiteLogo"),
                SiteFavIcon = _siteSettings.GetSetting("SiteFavIcon"),
                SiteUrl = _siteSettings.GetSetting("SiteUrl"),
                SiteTitle = _siteSettings.GetSetting("SiteTitle"),
                SiteMode = _siteSettings.GetSetting("SiteMode"),
                SiteColor = _siteSettings.GetSetting("SiteColor"),
                MetaTags = _siteSettings.GetSetting("MetaTags"),
                MetaDescription = _siteSettings.GetSetting("MetaDescription"),
                GoogleAnalytics = _siteSettings.GetSetting("GoogleAnalytics"),
                MainMenu = bool.Parse(_siteSettings.GetSetting("MainMenu")),
                PortfolioMenu = bool.Parse(_siteSettings.GetSetting("PortfolioMenu")),
                BlogMenu = bool.Parse(_siteSettings.GetSetting("BlogMenu")),
                ContactMeMenu = bool.Parse(_siteSettings.GetSetting("ContactMeMenu")),
                AboutMeMenu = bool.Parse(_siteSettings.GetSetting("AboutMeMenu")),

                //Site Profile
                FullName = _siteSettings.GetSetting("FullName"),
                Birthday = _siteSettings.GetSetting("Birthday"),
                Mobile = _siteSettings.GetSetting("Mobile"),
                AboutMe = _siteSettings.GetSetting("AboutMe"),
                Slogans = _siteSettings.GetSetting("Slogans"),
                Address = _siteSettings.GetSetting("Address"),
                PhoneNumber = _siteSettings.GetSetting("PhoneNumber"),
                Email = _siteSettings.GetSetting("Email"),
                Age = _siteSettings.GetSetting("Age"),
                Language = _siteSettings.GetSetting("Language"),
                Nationality = _siteSettings.GetSetting("Nationality"),
                ResumeFile = _siteSettings.GetSetting("ResumeFile"),
                ResumeImage = _siteSettings.GetSetting("ResumeImage"),
                AvatarImage = _siteSettings.GetSetting("AvatarImage"),

                //Social Icons
                Telegram = _siteSettings.GetSetting("Telegram"),
                Instagram = _siteSettings.GetSetting("Instagram"),
                Twitter = _siteSettings.GetSetting("Twitter"),
                Facebook = _siteSettings.GetSetting("Facebook"),
                Youtube = _siteSettings.GetSetting("Youtube"),
                Linkedin = _siteSettings.GetSetting("Linkedin")
            };

            return setting;
        }

        public void SetAllSetting(SiteSettingViewModel siteSetting)
        {
            //Site Settings
            _siteSettings.SetSetting(nameof(siteSetting.SiteLogo), siteSetting.SiteLogo);
            _siteSettings.SetSetting(nameof(siteSetting.SiteFavIcon), siteSetting.SiteFavIcon);
            _siteSettings.SetSetting(nameof(siteSetting.SiteMode),  siteSetting.SiteMode);
            _siteSettings.SetSetting(nameof(siteSetting.SiteColor),  siteSetting.SiteColor);
            _siteSettings.SetSetting(nameof(siteSetting.MetaTags), siteSetting.MetaTags);
            _siteSettings.SetSetting(nameof(siteSetting.MetaDescription), siteSetting.MetaDescription);
            _siteSettings.SetSetting(nameof(siteSetting.GoogleAnalytics), siteSetting.GoogleAnalytics);
            _siteSettings.SetSetting(nameof(siteSetting.MainMenu), siteSetting.MainMenu.ToString());
            _siteSettings.SetSetting(nameof(siteSetting.PortfolioMenu), siteSetting.PortfolioMenu.ToString());
            _siteSettings.SetSetting(nameof(siteSetting.BlogMenu), siteSetting.BlogMenu.ToString());
            _siteSettings.SetSetting(nameof(siteSetting.ContactMeMenu), siteSetting.ContactMeMenu.ToString());
            _siteSettings.SetSetting(nameof(siteSetting.AboutMeMenu), siteSetting.AboutMeMenu.ToString());
            _siteSettings.SetSetting(nameof(siteSetting.SiteUrl), siteSetting.SiteUrl);
            _siteSettings.SetSetting(nameof(siteSetting.SiteTitle), siteSetting.SiteTitle);

            //Site Profile
            _siteSettings.SetSetting(nameof(siteSetting.FullName), siteSetting.FullName);
            _siteSettings.SetSetting(nameof(siteSetting.Birthday), siteSetting.Birthday);
            _siteSettings.SetSetting(nameof(siteSetting.Mobile), siteSetting.Mobile);
            _siteSettings.SetSetting(nameof(siteSetting.AboutMe), siteSetting.AboutMe);
            _siteSettings.SetSetting(nameof(siteSetting.Slogans), siteSetting.Slogans);
            _siteSettings.SetSetting(nameof(siteSetting.ResumeImage), siteSetting.ResumeImage);
            _siteSettings.SetSetting(nameof(siteSetting.AvatarImage), siteSetting.AvatarImage);
            _siteSettings.SetSetting(nameof(siteSetting.ResumeFile), siteSetting.ResumeFile);
            _siteSettings.SetSetting(nameof(siteSetting.Address), siteSetting.Address);
            _siteSettings.SetSetting(nameof(siteSetting.PhoneNumber), siteSetting.PhoneNumber);
            _siteSettings.SetSetting(nameof(siteSetting.Email), siteSetting.Email);
            _siteSettings.SetSetting(nameof(siteSetting.Age), siteSetting.Age);
            _siteSettings.SetSetting(nameof(siteSetting.Language), siteSetting.Language);
            _siteSettings.SetSetting(nameof(siteSetting.Nationality), siteSetting.Nationality);

            //Social Icons
            _siteSettings.SetSetting(nameof(siteSetting.Telegram), siteSetting.Telegram);
            _siteSettings.SetSetting(nameof(siteSetting.Instagram), siteSetting.Instagram);
            _siteSettings.SetSetting(nameof(siteSetting.Twitter), siteSetting.Twitter);
            _siteSettings.SetSetting(nameof(siteSetting.Facebook), siteSetting.Facebook);
            _siteSettings.SetSetting(nameof(siteSetting.Youtube), siteSetting.Youtube);
            _siteSettings.SetSetting(nameof(siteSetting.Linkedin), siteSetting.Linkedin);
        }
    }
}
