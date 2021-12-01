using SaeedNA.Service.Repositories;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.SPost;
using System.Collections.Generic;
using System.Linq;

namespace SaeedNA.Service.Services
{
    public class CategoryService : ICategory
    {
        public readonly SaeedNAContext _context;

        public CategoryService(SaeedNAContext context)
        {
            _context = context;
        }

        public void DeleteCategory(int id)
        {
            var cat = _context.Categories.Find(id);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        ICollection<Category> ICategory.GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        Category ICategory.GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
