using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Pages;

namespace SaeedNA.Data.DTOs.Pages
{
    public class PortfolioCreateDTO
    {
        [Display(Name = "دسته بندی")]
        public long CategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "تصویر نوشته")]
        public string Imgae { get; set; }

        [Display(Name = "شرح کوتاه")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "برچسب")]
        public string Tags { get; set; }

        [Display(Name = "مشتری")]
        public string ProjectCustomer { get; set; }

        [Display(Name = "آدرس")]
        public string ProjectAddress { get; set; }

        [Display(Name = "زبان ها")]
        public string ProjectLanguage { get; set; }

        [Display(Name = "وضعیت")]
        public PortfolioPublishingState State { get; set; }
    }
}