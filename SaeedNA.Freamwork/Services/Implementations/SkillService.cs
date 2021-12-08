using SaeedNA.Data.Models.Resume;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class SkillService : ISkillService
    {
        public readonly IGenericRepository<Skill> _SkillRepository;

        public SkillService(IGenericRepository<Skill> skillRepository)
        {
            _SkillRepository = skillRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _SkillRepository.DisposeAsync();
        }
    }
}
