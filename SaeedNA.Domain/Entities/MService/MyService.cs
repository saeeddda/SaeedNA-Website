using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Common;

namespace SaeedNA.Data.Entities.MService
{
    public class MyService:BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Text { get; set; }

        [Display(Name = "آیکون")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Icon { get; set; }
    }
}
