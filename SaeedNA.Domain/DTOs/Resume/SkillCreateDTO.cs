using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Resume
{
    public class SkillCreateDTO
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "درصد پیشرفت")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(3, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Progress { get; set; }
    }
}