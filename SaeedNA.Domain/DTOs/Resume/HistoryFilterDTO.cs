using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Resume;

namespace SaeedNA.Data.DTOs.Resume
{
    public class HistoryFilterDTO : BasePaging
    {
        public string Title { get; set; }
        public bool IsDescending { get; set; }
        public bool IsDelete { get; set; }
        public List<History> Histories { get; set; }

        public HistoryFilterDTO SetHistory(List<History> histories)
        {
            this.Histories = histories;
            return this;
        }

        public HistoryFilterDTO SetPaging(BasePaging paging)
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