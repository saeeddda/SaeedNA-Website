using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        public readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _categoryRepository.DisposeAsync();
        }

        public async Task<CategoryStatusResult> AddNewCategory(CategoryCreateDTO category)
        {
            try
            {
                var entity = new Category
                {
                    IsDelete = false,
                    Name = category.Name
                };

                var result = await _categoryRepository.AddEntity(entity);
                
                return result ? CategoryStatusResult.Success : CategoryStatusResult.Error;
            }
            catch
            {
                return CategoryStatusResult.Error;
            }
        }

        public async Task<CategoryStatusResult> EditCategory(CategoryEditDTO category)
        {
            try
            {

                var entity = new Category
                {
                    IsDelete = false,
                    Id = category.Id,
                    Name = category.Name,
                };

                var result = _categoryRepository.EditEntity(entity);

                await Task.CompletedTask;

                return result ? CategoryStatusResult.Success : CategoryStatusResult.Error;
            }
            catch
            {
                return CategoryStatusResult.Error;
            }
        }

        public async Task<CategoryStatusResult> DeleteCategory(long categoryId)
        {
            try
            {
                var result = await _categoryRepository.DeleteEntity(categoryId);

                return result ? CategoryStatusResult.Success : CategoryStatusResult.Error;
            }
            catch
            {
                return CategoryStatusResult.Error;
            }
        }
        
        public async Task<Category> GetCategoryById(long categoryId)
        {
            return await _categoryRepository.GetQuery().SingleOrDefaultAsync(s => s.Id==categoryId &&  !s.IsDelete);
        }
    }
}
