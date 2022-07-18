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
    public class SeoService : ISeoService
    {
        private readonly IGenericRepository<Seo> _seoRepository;

        public SeoService(IGenericRepository<Seo> seoRepository)
        {
            _seoRepository = seoRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _seoRepository.DisposeAsync();
        }

        public async Task<ServiceResult> AddNewSeo(SeoCreateDTO seo)
        {
            try
            {
                var entity = new Seo
                {
                    MetaTags = seo.MetaTags,
                    MetaDescription = seo.MetaDescription,
                    Canonical = seo.Canonical,
                    Author = seo.Author,
                    Publisher = seo.Publisher,
                    GoogleAnalytics = seo.GoogleAnalytics,
                    SiteMap = seo.SiteMap,
                    RobotsTxt = seo.RobotsTxt,
                    IsDefault = seo.IsDefault
                };

                var result = await _seoRepository.AddEntity(entity);
                await _seoRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditSeo(SeoGetSetDTO seo)
        {
            try
            {
                var entity = await _seoRepository.GetEntityById(seo.SeoId);
                if (entity == null) return ServiceResult.NotFond;

                entity.MetaTags = seo.MetaTags;
                entity.MetaDescription = seo.MetaDescription;
                entity.Canonical = seo.Canonical;
                entity.Author = seo.Author;
                entity.Publisher = seo.Publisher;
                entity.GoogleAnalytics = seo.GoogleAnalytics;
                entity.SiteMap = seo.SiteMap;
                entity.RobotsTxt = seo.RobotsTxt;
                entity.IsDefault = seo.IsDefault;

                var result = _seoRepository.EditEntity(entity);
                await _seoRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteSeo(long seoId)
        {
            try
            {
                var entity = await _seoRepository.GetEntityById(seoId);
                if (entity == null) return ServiceResult.NotFond;

                var result = _seoRepository.DeleteEntity(entity);
                await _seoRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<SeoFilterDTO> FilterSeo(SeoFilterDTO filter)
        {
            var query = _seoRepository.GetQuery().AsQueryable();

            query = query.Where(s => s.IsDelete == filter.IsDelete);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetSeo(allEntities).SetPaging(pager);
        }

        public async Task<SeoGetSetDTO> GetDefaultSeo()
        {
            var query = await _seoRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(s => s.IsDefault && !s.IsDelete);

            if (query == null) return null;

            return new SeoGetSetDTO
            {
                Author = query.Author,
                Canonical = query.Canonical,
                GoogleAnalytics = query.GoogleAnalytics,
                IsDefault = query.IsDefault,
                MetaDescription = query.MetaDescription,
                MetaTags = query.MetaTags,
                Publisher = query.Publisher,
                RobotsTxt = query.RobotsTxt,
                SeoId = query.Id,
                SiteMap = query.SiteMap
            };
        }

        public async Task<ServiceResult> SetDefaultSeo(SeoGetSetDTO seo)
        {
            try
            {
                ServiceResult isEdited = ServiceResult.NotFond;
                var backupSeo = new SeoGetSetDTO();

                #region Delete Old Data

                if (seo.SeoId > 0)
                {
                    var seoData = await _seoRepository.GetQuery().AsQueryable()
                        .SingleOrDefaultAsync(s => s.IsDefault && !s.IsDelete && s.Id == seo.SeoId);

                    var oldSeo = new SeoGetSetDTO
                    {
                        SeoId = seoData.Id,
                        IsDefault = false,
                        Author = seoData.Author,
                        Canonical = seoData.Canonical,
                        GoogleAnalytics = seoData.GoogleAnalytics,
                        MetaDescription = seoData.MetaDescription,
                        MetaTags = seoData.MetaTags,
                        Publisher = seoData.Publisher,
                        RobotsTxt = seoData.RobotsTxt,
                        SiteMap = seoData.SiteMap
                    };

                    backupSeo = oldSeo;

                    isEdited = (await EditSeo(oldSeo) == ServiceResult.Success) &&
                        (await DeleteSeo(seo.SeoId) == ServiceResult.Success) ? ServiceResult.Success : ServiceResult.Error;
                }

                #endregion

                #region Add New Data

                var data = new SeoCreateDTO
                {
                    Author = string.IsNullOrEmpty(seo.Author) ? backupSeo.Author : seo.Author,
                    Canonical = string.IsNullOrEmpty(seo.Canonical) ? backupSeo.Canonical : seo.Canonical,
                    GoogleAnalytics = string.IsNullOrEmpty(seo.GoogleAnalytics) ? backupSeo.GoogleAnalytics : seo.GoogleAnalytics,
                    MetaDescription = string.IsNullOrEmpty(seo.MetaDescription) ? backupSeo.MetaDescription : seo.MetaDescription,
                    MetaTags = string.IsNullOrEmpty(seo.MetaTags) ? backupSeo.MetaTags : seo.MetaTags,
                    Publisher = string.IsNullOrEmpty(seo.Publisher) ? backupSeo.Publisher : seo.Publisher,
                    IsDefault = true
                };

                var addResult = await AddNewSeo(data) == ServiceResult.Success ? ServiceResult.Success : ServiceResult.Error;

                #endregion

                return isEdited == ServiceResult.Success && addResult == ServiceResult.Success ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }
    }
}