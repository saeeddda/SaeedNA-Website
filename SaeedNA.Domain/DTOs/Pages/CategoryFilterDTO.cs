using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Pages;

namespace SaeedNA.Data.DTOs.Pages
{
    public class CategoryFilterDTO : BasePaging
    {
        public string Name { get; set; }

        public List<Category> Categories { get; set; }

        public CategoryFilterDTO SetCategory(List<Category> categories)
        {
            this.Categories = categories;
            return this;
        }

        public CategoryFilterDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;
            this.HowManyBeforeAndAfter = paging.HowManyBeforeAndAfter;
            return this;
        }
    }
}