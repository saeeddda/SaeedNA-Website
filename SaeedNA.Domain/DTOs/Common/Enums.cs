using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Common
{
    public enum ServiceResult
    {
        Success,
        Error,
        NotFond
    }

    public enum PagesPublishState
    {
        [Display(Name = "پیش نویس")]
        Draft,
        [Display(Name = "منتشر شده")]
        Published,
        [Display(Name = "در انتظار")]
        Pending
    }
}