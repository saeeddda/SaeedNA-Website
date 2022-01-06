using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.MService;

namespace SaeedNA.Data.DTOs.MService
{
    public class CounterFilterDTO:BasePaging
    {
        public string Title { get; set; }
        public bool IsDelete { get; set; }

        public List<Counter> Counters { get; set; }

        public CounterFilterDTO SetCounter(List<Counter> counters)
        {
            this.Counters = counters;
            return this;
        }

        public CounterFilterDTO SetPaging(BasePaging paging)
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