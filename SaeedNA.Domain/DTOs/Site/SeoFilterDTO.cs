using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Data.DTOs.Site
{
    public class SeoFilterDTO:BasePaging
    {
        public bool IsDelete { get; set; }
        
        public List<Seo> Seos { get; set; }

        public SeoFilterDTO SetSeo(List<Seo> seo)
        {
            this.Seos = seo;
            return this;
        }

        public SeoFilterDTO SetPaging(BasePaging paging)
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