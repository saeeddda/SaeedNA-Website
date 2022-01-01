using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Common
{
    public enum ServiceResult
    {
        Success,
        Error,
        NotFond,
        Exist,
        NotMatch
    }

    public enum PagesPublishState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "پیش نویس")]
        Draft,
        [Display(Name = "منتشر شده")]
        Published,
    }

    public enum SiteMode
    {
        Dark,
        Light
    }
}