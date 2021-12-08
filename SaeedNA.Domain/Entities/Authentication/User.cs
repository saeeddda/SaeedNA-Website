using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Common;

namespace SaeedNA.Data.Entities.Authentication
{
    public class User:BaseEntity
    {
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }
    }
}