using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Resume;

namespace SaeedNA.Data.DTOs.Resume
{
    public class SkillFilterDTO : BasePaging
    {
        public string Title { get; set; }

        public List<Skill> Skills { get; set; }

        public SkillFilterDTO SetSkill(List<Skill> skills)
        {
            this.Skills = skills;
            return this;
        }

        public SkillFilterDTO SetPaging(BasePaging paging)
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