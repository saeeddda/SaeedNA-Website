using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Common;

namespace SaeedNA.Data.Entities.Resume
{
    public class History : BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "برای چه کسی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string WorkPlace { get; set; }

        [Display(Name = "تاریخ")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Date { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }
    }
}
