using SaeedNA.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.Entities.Pages
{
    public class Post:BaseEntity
    {
        public Post()
        {

        }

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

        [Display(Name = "شرح")]
        public string Description { get; set; }

        [Display(Name = "برچسب")]
        public string Tags { get; set; }

        public int Visit { get; set; }

        [Display(Name = "وضعیت")]
        public PostPublishingState State { get; set; }

        #region Relations

        public Category Category { get; set; }

        #endregion
    }
    public enum PostPublishingState
    {
        [Display(Name = "پیشنویس")]
        Draft,
        [Display(Name = "منتشر شده")]
        Published
    }
}
