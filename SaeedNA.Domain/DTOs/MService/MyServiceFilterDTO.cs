using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.MService;

namespace SaeedNA.Data.DTOs.MService
{
    public class MyServiceFilterDTO : BasePaging
    {
        public string Title { get; set; }
        public bool  IsDelete { get; set; }

        public List<MyService> MyServices { get; set; }

        public MyServiceFilterDTO SetMyService(List<MyService> myServices)
        {
            this.MyServices = myServices;
            return this;
        }

        public MyServiceFilterDTO SetPaging(BasePaging paging)
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