using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Data.DTOs.Site
{
    public class SocialMediaFilterDTO : BasePaging
    {
        public List<SocialMedia> SocialMediae { get; set; }

        public SocialMediaFilterDTO SetSocialMedia(List<SocialMedia> socialMedia)
        {
            this.SocialMediae = socialMedia;
            return this;
        }

        public SocialMediaFilterDTO SetPaging(BasePaging paging)
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