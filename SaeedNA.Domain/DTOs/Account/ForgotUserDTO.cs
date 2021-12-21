using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Account
{
    public class ForgotUserDTO
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "کد بازیابی")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ForgotToken { get; set; }
    }
}