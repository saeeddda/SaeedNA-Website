using SaeedNA.Domain.Models.SPost;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface ICategory
    {
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        ICollection<Category> GetAllCategory();
        Category GetCategoryById(int id);
        void DeleteCategory(int id);
    }
}
