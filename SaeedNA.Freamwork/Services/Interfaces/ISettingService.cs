using System;
using System.Threading.Tasks;
using SaeedNA.Data.Models.Settings;

namespace SaeedNA.Service.Interfaces
{
    public interface ISettingService:IAsyncDisposable
    {
        Task<Setting> GetDefaultSetting();
    }
}
