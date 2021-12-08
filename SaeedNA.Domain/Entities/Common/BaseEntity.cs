using System;
using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Display(Name ="حذف شده")]
        public bool IsDelete { get; set; }
        
        [Display(Name ="تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public DateTime CreateDate { get; set; }
        
        [Display(Name ="آخرین ویرایش")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public DateTime LastUpdateDate { get; set; }
    }
}