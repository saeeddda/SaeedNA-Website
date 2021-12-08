using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Common;

namespace SaeedNA.Data.Entities.Resume
{
    public class Skill:BaseEntity
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
