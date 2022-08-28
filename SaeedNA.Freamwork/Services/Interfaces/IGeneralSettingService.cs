using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Service.Interfaces
{
    public interface IGeneralSettingService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewSetting(SettingCreateDTO setting);
        Task<ServiceResult> EditSetting(SettingGetSetDTO setting);
        Task<ServiceResult> DeleteSetting(long settingId);
        Task<SettingFilterDTO> FilterSetting(SettingFilterDTO filter);
        Task<SettingGetSetDTO> GetDefaultSetting();
        Task<ServiceResult> SetDefaultSetting(SettingGetSetDTO setting);
    }
}
