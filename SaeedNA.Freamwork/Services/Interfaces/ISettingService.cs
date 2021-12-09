using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;

namespace SaeedNA.Service.Interfaces
{
    public interface ISettingService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewSetting(SettingCreateDTO setting);
        Task<ServiceResult> EditSetting(SettingEditDTO setting);
        Task<ServiceResult> DeleteSetting(long settingId);
        Task<SettingFilterDTO> FilterSetting(SettingFilterDTO filter);
        Task<SettingEditDTO> GetDefaultSetting();
    }
}
