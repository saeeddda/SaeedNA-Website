using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Site
{
    public class SettingCreateDTO
    {
        [Display(Name = "لوگو سایت")]
        public string SiteLogo { get; set; }

        [Display(Name = "فاو آیکون")]
        public string SiteFavIcon { get; set; }

        [Display(Name = "آدرس سایت")]
        public string SiteUrl { get; set; }

        [Display(Name = "عنوان سایت")]
        public string SiteTitle { get; set; }

        [Display(Name = "حالت سایت")]
        public string SiteMode { get; set; }
    }
}