using SaeedNA.Data.Models.Settings;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaeedNA.Service.Implementations
{
    public class SettingsService : ISettingService
    {
        public readonly IGenericRepository<Setting> _settingRepository;

        public SettingsService(IGenericRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _settingRepository.DisposeAsync();
        }

        public async Task<Setting> GetDefaultSetting()
        {
            return await _settingRepository.GetQuery().SingleOrDefaultAsync(s => s.IsDefault);
        }
    }
}
