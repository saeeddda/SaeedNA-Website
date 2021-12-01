using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.Resume
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string SkillTitle { get; set; }

        [Display(Name = "درصد پیشرفت")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(3, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string SkillProgress { get; set; }
    }
}
