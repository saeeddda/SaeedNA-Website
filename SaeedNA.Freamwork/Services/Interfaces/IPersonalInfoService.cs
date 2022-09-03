using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface IPersonalInfoService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewInfo(PersonalInfoCreateDTO info);
        Task<ServiceResult> EditInfo(PersonalInfoGetSetDTO info);
        Task<ServiceResult> DeleteInfo(long infoId);
        Task<PersonalInfoFilterDTO> FilterInfo(PersonalInfoFilterDTO filter);
        Task<PersonalInfoGetSetDTO> GetInfoById(long infoId);
        Task<ICollection<PersonalInfoGetSetDTO>> GetDefaultInfo();
        Task<ServiceResult> SetDefaultPersonalInfo(long infoId);
    }
}