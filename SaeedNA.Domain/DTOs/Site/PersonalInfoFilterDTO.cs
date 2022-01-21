using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Data.DTOs.Site
{
    public class PersonalInfoFilterDTO:BasePaging
    {
        public bool IsDelete { get; set; }
        public List<PersonalInfo> PersonalInfos { get; set; }

        public PersonalInfoFilterDTO SetPersonalInfo(List<PersonalInfo> info)
        {
            this.PersonalInfos = info;
            return this;
        }

        public PersonalInfoFilterDTO SetPaging(BasePaging paging)
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