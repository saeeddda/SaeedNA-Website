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
    public class GeneralSettingsService : IGeneralSettingService
    {
        private readonly IGenericRepository<Setting> _settingRepository;

        public GeneralSettingsService(IGenericRepository<Setting> settingRepository)
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

        public async Task<ServiceResult> EditSetting(SettingGetSetDTO setting)
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
                if (entity == null) return ServiceResult.NotFond;

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

            query = query.Where(s => s.IsDelete == filter.IsDelete);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetSetting(allEntities).SetPaging(pager);
        }

        public async Task<SettingGetSetDTO> GetSettingById(long settingId)
        {
            var query = await _settingRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(s => s.Id == settingId && !s.IsDelete);

            if (query == null) return null;

            return new SettingGetSetDTO
            {
                IsDefault = query.IsDefault,
                SettingId = query.Id,
                SiteFavIcon = query.SiteFavIcon,
                SiteLogo = query.SiteLogo,
                SiteMode = query.SiteMode,
                SiteTitle = query.SiteTitle,
                SiteUrl = query.SiteUrl
            };
        }

        public async Task<ServiceResult> SetDefaultSetting(long settingId)
        {
            try
            {
                if (settingId == 0) return ServiceResult.Error;

                var oldEntity = await _settingRepository.GetQuery().AsQueryable()
                    .Where(s => s.IsDefault).ToListAsync();
                foreach (var oe in oldEntity)
                {
                    oe.IsDefault = false;
                    _settingRepository.EditEntity(oe);
                    await _settingRepository.SaveChanges();
                }

                var entity = await _settingRepository.GetEntityById(settingId);
                if (entity == null) return ServiceResult.NotFond;

                entity.IsDefault = true;

                var result = _settingRepository.EditEntity(entity);
                await _settingRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }
    }
}
