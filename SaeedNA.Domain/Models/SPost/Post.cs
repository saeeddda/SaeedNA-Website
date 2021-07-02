using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.SPost
{
    public class Post
    {
        public Post()
        {

        }

        [Key]
        public int PostId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PostTitle { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public string PostCreateDate { get; set; }

        [Display(Name = "شرح کوتاه")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PostShortDescription { get; set; }

        [Display(Name = "شرح")]
        public string PostDescription { get; set; }

        [Display(Name = "برچسب")]
        public string PostTags { get; set; }

        [Display(Name = "مشتری")]
        public string ProjectCustomer { get; set; }

        [Display(Name = "آدرس")]
        public string ProjectAddress { get; set; }

        [Display(Name = "زبان ها")]
        public string ProjectLanguage { get; set; }
        
        [Display(Name = "نوع نوشته")]
        public string PostType { get; set; }

        [Display(Name = "منتشر شده ؟")]
        public bool IsPublished { get; set; }

        public int PostVisit { get; set; }

        [Display(Name = "فایل نوشته")]
        public string PostFile { get; set; }

        [Display(Name = "دسته بندی")]
        public int CategoryId { get; set; }

        #region Relations

        public Category Category { get; set; }

        #endregion
    }
}
