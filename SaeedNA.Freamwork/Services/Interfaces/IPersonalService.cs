using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;

namespace SaeedNA.Service.Interfaces
{
    public interface IPersonalService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewInfo(PersonalInfoCreateDTO info);
        Task<ServiceResult> EditInfo(PersonalInfoEditDTO info);
        Task<ServiceResult> DeleteInfo(long infoId);
        Task<PersonalInfoFilterDTO> FilterInfo(PersonalInfoFilterDTO filter);
        Task<PersonalInfoEditDTO> GetDefaultInfo();
    }
}