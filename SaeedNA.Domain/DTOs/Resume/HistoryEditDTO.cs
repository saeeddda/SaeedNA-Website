using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Resume
{
    public class HistoryEditDTO
    {
        public long HistoryId { get; set; }

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