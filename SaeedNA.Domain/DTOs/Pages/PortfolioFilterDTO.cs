using System.Collections.Generic;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Pages;

namespace SaeedNA.Data.DTOs.Pages
{
    public class PortfolioFilterDTO:BasePaging
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDescending { get; set; }
        public PagesPublishState State { get; set; }

        public List<Portfolio> Portfolios { get; set; }

        public PortfolioFilterDTO SetPortfolio(List<Portfolio> portfolios)
        {
            this.Portfolios = portfolios;
            return this;
        }

        public PortfolioFilterDTO SetPaging(BasePaging paging)
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