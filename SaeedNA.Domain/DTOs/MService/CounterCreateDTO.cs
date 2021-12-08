using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.MService
{
    public class CounterCreateDTO
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Number { get; set; }
    }
}