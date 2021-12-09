using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface ICategoryService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewCategory(CategoryCreateDTO category);
        Task<ServiceResult> EditCategory(CategoryEditDTO category);
        Task<ServiceResult> DeleteCategory(long categoryId); 
        Task<CategoryEditDTO> EditCategoryById(long categoryId);
        Task<CategoryFilterDTO> FilterCategory(CategoryFilterDTO filter);
    }
}
