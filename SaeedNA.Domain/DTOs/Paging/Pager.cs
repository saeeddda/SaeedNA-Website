using System;

namespace SaeedNA.Data.DTOs.Paging
{
    public class Pager
    {
        public static BasePaging Build(int pageId, int allEntitiesCount, int take, int howManyBeforeAndAfter)
        {
            var pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)take));

            return new BasePaging
            {
                PageId = pageId,
                AllEntitiesCount = allEntitiesCount,
                TakeEntity = take,
                SkipEntity = (pageId - 1) * take,
                StartPage = pageId - howManyBeforeAndAfter <= 0 ? 1 : pageId - howManyBeforeAndAfter,
                EndPage = pageId + howManyBeforeAndAfter > pageCount ? pageCount : pageId + howManyBeforeAndAfter,
                HowManyBeforeAndAfter = howManyBeforeAndAfter,
                PageCount = pageCount
            };
        }
    }
}