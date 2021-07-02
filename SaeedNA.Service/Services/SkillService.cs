using SaeedNA.Service.Repositories;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.Resume;
using System.Collections.Generic;
using System.Linq;

namespace SaeedNA.Service.Services
{
    public class SkillService : ISkill
    {
        public readonly SaeedNAContext _context;

        public SkillService(SaeedNAContext context)
        {
            _context = context;
        }

        public void AddSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
        }

        public void DeleteSkill(int id)
        {
            var skill = _context.Skills.Find(id);
            _context.Skills.Remove(skill);
            _context.SaveChanges();
        }

        public ICollection<Skill> GetAllSkill()
        {
            return _context.Skills.ToList();
        }

        public Skill GetSkillById(int id)
        {
            return _context.Skills.FirstOrDefault(s => s.SkillId == id);
        }

        public void UpdateSkill(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
        }
    }
}
