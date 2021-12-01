using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.MService
{
    public class ServiceCounter
    {
        [Key]
        public int ServiceCounterId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ServiceCounterTitle { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ServiceCounterCount { get; set; }
    }
}
