using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.DTOs.Resume;
using SaeedNA.Data.Entities.Resume;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly IGenericRepository<Skill> _skillRepository;

        public SkillService(IGenericRepository<Skill> skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _skillRepository.DisposeAsync();
        }

        public async Task<ServiceResult> AddNewSkill(SkillCreateDTO skill)
        {
            try
            {
                var entity = new Skill
                {
                    Title = skill.Title,
                    Progress = skill.Progress
                };

                var result = await _skillRepository.AddEntity(entity);
                await _skillRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditSkill(SkillEditDTO skill)
        {
            try
            {
                var entity = await _skillRepository.GetEntityById(skill.SkillId);
                if(entity == null) return ServiceResult.NotFond;

                entity.Title = skill.Title;
                entity.Progress = skill.Progress;

                var result = _skillRepository.EditEntity(entity);
                await _skillRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteSkill(long skillId)
        {
            try
            {
                var entity = await _skillRepository.GetEntityById(skillId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _skillRepository.DeleteEntity(entity);
                await _skillRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<SkillFilterDTO> FilterSkill(SkillFilterDTO filter)
        {
            var query = _skillRepository.GetQuery().AsQueryable();

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetSkill(allEntities).SetPaging(pager);
        }
    }
}
