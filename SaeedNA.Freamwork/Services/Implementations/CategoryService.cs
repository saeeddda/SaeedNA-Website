using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _categoryRepository.DisposeAsync();
        }

        public async Task<ServiceResult> AddNewCategory(CategoryCreateDTO category)
        {
            try
            {
                var entity = new Category
                {
                    IsDelete = false,
                    Name = category.Name
                };

                var result = await _categoryRepository.AddEntity(entity);
                await _categoryRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditCategory(CategoryEditDTO category)
        {
            try
            {
                var entity = await _categoryRepository.GetEntityById(category.CategoryId);
                if (entity == null) return ServiceResult.NotFond;

                entity.Name= category.Name;

                var result = _categoryRepository.EditEntity(entity);
                await _categoryRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteCategory(long categoryId)
        {
            try
            {
                var entity = await _categoryRepository.GetEntityById(categoryId);
                if(entity == null) return ServiceResult.NotFond;

                var result = await _categoryRepository.DeleteEntity(categoryId);
                await _categoryRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }
        
        public async Task<CategoryEditDTO> EditCategoryById(long categoryId)
        {
            var entity = await _categoryRepository.GetQuery()
                .SingleOrDefaultAsync(s => s.Id == categoryId && !s.IsDelete);
            if (entity == null) return null;

            return new CategoryEditDTO()
            {
                CategoryId = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<CategoryFilterDTO> FilterCategory(CategoryFilterDTO filter)
        {
            var query = _categoryRepository.GetQuery().AsQueryable();

            query = query.Where(s => s.IsDelete == filter.IsDelete);

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(s => EF.Functions.Like(s.Name, $"%{filter.Name}%"));

            var pager = Pager.Build(filter.PageId,await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetCategory(allEntities).SetPaging(pager);
        }
    }
}
