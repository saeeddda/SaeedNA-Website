using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Common;

namespace SaeedNA.Data.Entities.Settings
{
    public class Setting : BaseEntity
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

        [Display(Name = "پیشفرض")]
        public bool IsDefault { get; set; }
    }
}