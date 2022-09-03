using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface IGeneralSettingService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewSetting(SettingCreateDTO setting);
        Task<ServiceResult> EditSetting(SettingGetSetDTO setting);
        Task<ServiceResult> DeleteSetting(long settingId);
        Task<SettingFilterDTO> FilterSetting(SettingFilterDTO filter);
        Task<SettingGetSetDTO> GetSettingById(long settingId);
        Task<ICollection<SettingGetSetDTO>> GetDefaultSetting();
        Task<ServiceResult> SetDefaultSetting(long settingId);
    }
}
