using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.MService
{
    public class MyServiceEditDTO
    {
        public long MyServiceId { get; set; }

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