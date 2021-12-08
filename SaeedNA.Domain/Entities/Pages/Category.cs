using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SaeedNA.Data.Entities.Common;

namespace SaeedNA.Data.Entities.Pages
{
    public class Category:BaseEntity
    {
        public Category()
        {

        }
        
        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        public int PostId { get; set; }

        #region Relations

        public List<Post> Post { get; set; }
        public List<Portfolio> Portfolio { get; set; }

        #endregion
    }
}
