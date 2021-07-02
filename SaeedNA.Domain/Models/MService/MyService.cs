using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.MService
{
    public class MyService
    {
        [Key]
        public int MyServiceId { get; set; }

        [Display(Name = "عنوان خدمت")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MyServiceTitle { get; set; }

        [Display(Name = "متن خدمت")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MyServiceText { get; set; }
    }
}
