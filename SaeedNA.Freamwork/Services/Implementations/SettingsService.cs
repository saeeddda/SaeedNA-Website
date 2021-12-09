using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.Entities.Settings;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ServiceResult> AddNewSetting(SettingCreateDTO setting)
        {
            try
            {
                var entity = new Setting
                {
                    SiteTitle = setting.SiteTitle,
                    SiteLogo = setting.SiteLogo,
                    SiteUrl = setting.SiteUrl,
                    SiteFavIcon = setting.SiteFavIcon,
                    SiteMode = setting.SiteMode,
                    IsDefault = setting.IsDefault
                };

                var result = await _settingRepository.AddEntity(entity);
                await _settingRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditSetting(SettingEditDTO setting)
        {
            try
            {
                var entity = await _settingRepository.GetEntityById(setting.SettingId);
                if (entity == null) return ServiceResult.NotFond;

                entity.SiteTitle = setting.SiteTitle;
                entity.SiteLogo = setting.SiteLogo;
                entity.SiteUrl = setting.SiteUrl;
                entity.SiteFavIcon = setting.SiteFavIcon;
                entity.SiteMode = setting.SiteMode;
                entity.IsDefault = setting.IsDefault;
             
                var result = _settingRepository.EditEntity(entity);
                await _settingRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteSetting(long settingId)
        {
            try
            {
                var entity = await _settingRepository.GetEntityById(settingId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _settingRepository.DeleteEntity(entity);
                await _settingRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<SettingFilterDTO> FilterSetting(SettingFilterDTO filter)
        {
            var query = _settingRepository.GetQuery().AsQueryable();

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetSetting(allEntities).SetPaging(pager);
        }

        public async Task<SettingEditDTO> GetDefaultSetting()
        {
            return await _settingRepository.GetQuery()
                .Select(s => new SettingEditDTO())
                .SingleOrDefaultAsync(s => s.IsDefault);
        }
    }
}
