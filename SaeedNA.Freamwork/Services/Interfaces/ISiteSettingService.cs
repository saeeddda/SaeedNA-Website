using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Service.Interfaces
{
    public interface ISiteSettingService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewSetting(SettingCreateDTO setting);
        Task<ServiceResult> EditSetting(SettingEditDTO setting);
        Task<ServiceResult> DeleteSetting(long settingId);
        Task<SettingFilterDTO> FilterSetting(SettingFilterDTO filter);
        Task<Setting> GetDefaultSetting();
    }
}
