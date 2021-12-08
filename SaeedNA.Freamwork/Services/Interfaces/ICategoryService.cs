using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Data.Entities.Pages;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface ICategoryService:IAsyncDisposable
    {
        Task<CategoryStatusResult> AddNewCategory(CategoryCreateDTO category);
        Task<CategoryStatusResult> EditCategory(CategoryEditDTO category);
        Task<CategoryStatusResult> DeleteCategory(long categoryId); 
        Task<Category> GetCategoryById(long categoryId);
    }
}
