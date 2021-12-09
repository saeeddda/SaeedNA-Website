using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Resume;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface ISkillService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewSkill(SkillCreateDTO skill);
        Task<ServiceResult> EditSkill(SkillEditDTO skill);
        Task<ServiceResult> DeleteSkill(long skillId);
        Task<SkillFilterDTO> FilterSkill(SkillFilterDTO filter);
    }
}
