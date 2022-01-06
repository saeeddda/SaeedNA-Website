using System;
using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Pages
{
    public class PostShowDTO
    {
        public long PostId { get; set; }

        [Display(Name = "دسته بندی")]
        public string CategoryName { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "تصویر نوشته")]
        public string Image { get; set; }

        [Display(Name = "شرح کوتاه")]
        public string ShortDescription { get; set; }

        [Display(Name = "شرح")]
        public string Description { get; set; }

        [Display(Name = "برچسب")]
        public string Tags { get; set; }

        [Display(Name = "بازدید")]
        public int Visit { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        
        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime LastUpdateDate { get; set; }
    }
}
