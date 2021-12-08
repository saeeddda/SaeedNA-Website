using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Pages
{
    public class CategoryCreateDTO
    {
        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }
    }
}