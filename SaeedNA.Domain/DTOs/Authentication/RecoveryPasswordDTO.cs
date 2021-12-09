using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Authentication
{
    public class RecoveryPasswordDTO
    {
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "کد بازیابی")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ForgotToken { get; set; }
    }
}