using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Pages
{
    public class CategoryEditDTO
    {
        public long CategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }
    }
}