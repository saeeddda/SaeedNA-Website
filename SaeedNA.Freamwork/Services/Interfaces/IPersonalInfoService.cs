using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Service.Interfaces
{
    public interface IPersonalInfoService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewInfo(PersonalInfoCreateDTO info);
        Task<ServiceResult> EditInfo(PersonalInfoGetSetDTO info);
        Task<ServiceResult> DeleteInfo(long infoId);
        Task<PersonalInfoFilterDTO> FilterInfo(PersonalInfoFilterDTO filter);
        Task<PersonalInfoGetSetDTO> GetDefaultInfo();
        Task<ServiceResult> SetDefaultPersonalInfo(PersonalInfoGetSetDTO personalInfo);
    }
}