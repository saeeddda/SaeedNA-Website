using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Site
{
    public class SeoCreateDTO
    {
        [Display(Name = "تگ های سئو")]
        public string MetaTags { get; set; }

        [Display(Name = "توضیحات سئو")]
        public string MetaDescription { get; set; }

        [Display(Name = "کنونیکال")]
        public string Canonical { get; set; }

        [Display(Name = "نویسنده")]
        public string Author { get; set; }

        [Display(Name = "منتشرکننده")]
        public string Publisher { get; set; }

        [Display(Name = "کد گوگل آنالایتیکس")]
        public string GoogleAnalytics { get; set; }
    }
}