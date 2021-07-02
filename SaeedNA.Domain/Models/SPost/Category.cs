using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.SPost
{
    public class Category
    {
        public Category()
        {

        }

        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string CategoryName { get; set; }

        public int PostId { get; set; }

        #region Relations

        public List<Post> Post { get; set; }

        #endregion
    }
}
