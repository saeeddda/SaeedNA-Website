using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Pages;
using System.Collections.Generic;

namespace SaeedNA.Data.DTOs.Pages
{
    public class PostFilterDTO : BasePaging
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public bool IsDescending { get; set; }
        public bool IsDelete { get; set; }
        public PagesPublishState State { get; set; }

        public List<Post> Posts { get; set; }

        public PostFilterDTO SetPost(List<Post> posts)
        {
            this.Posts = posts;
            return this;
        }

        public PostFilterDTO SetPaging(BasePaging paging)
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