using SaeedNA.Domain.Models.Resume;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface ISkill
    {
        void AddSkill(Skill skill);
        void UpdateSkill(Skill skill);
        ICollection<Skill> GetAllSkill();
        Skill GetSkillById(int id);
        void DeleteSkill(int id);
    }
}
