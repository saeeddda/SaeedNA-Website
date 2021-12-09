using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Pages;

namespace SaeedNA.Data.DTOs.Pages
{
    public class PostEditDTO
    {
        public long PostId { get; set; }

        [Display(Name = "دسته بندی")]
        public long CategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "تصویر نوشته")]
        public string Image { get; set; }

        [Display(Name = "شرح کوتاه")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "شرح")]
        public string Description { get; set; }

        [Display(Name = "برچسب")]
        public string Tags { get; set; }

        public int Visit { get; set; }

        [Display(Name = "وضعیت")]
        public PostPublishingState State { get; set; }
    }
}