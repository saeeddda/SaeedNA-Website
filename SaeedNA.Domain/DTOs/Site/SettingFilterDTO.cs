using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Data.DTOs.Site
{
    public class SettingFilterDTO : BasePaging
    {
        public bool IsDelete { get; set; }
        public bool IsDefault { get; set; }
        public List<Setting> Settings { get; set; }

        public SettingFilterDTO SetSetting(List<Setting> setting)
        {
            this.Settings = setting;
            return this;
        }

        public SettingFilterDTO SetPaging(BasePaging paging)
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