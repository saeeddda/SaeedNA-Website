using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Site
{
    public class SocialMediaCreateDTO
    {
        [Display(Name = "آیکون")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MediaIcon { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MediaLink { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MedialName { get; set; }
    }
}