using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.Entities.Settings;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Collections.Generic;
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

        public async Task<SeoGetSetDTO> GetSeoById(long seoId)
        {
            var query = await _seoRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(s => s.Id == seoId && !s.IsDelete);

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

        public async Task<ICollection<SeoGetSetDTO>> GetDefaultSeo()
        {
            var query = await _seoRepository.GetQuery().AsQueryable()
                .Where(s => s.IsDefault && !s.IsDelete).ToListAsync();

            if (query == null) return null;

            var result = new List<SeoGetSetDTO>();

            foreach (var item in query)
            {
                result.Add(new SeoGetSetDTO()
                {
                    Author = item.Author,
                    Canonical = item.Canonical,
                    GoogleAnalytics = item.GoogleAnalytics,
                    IsDefault = item.IsDefault,
                    MetaDescription = item.MetaDescription,
                    MetaTags = item.MetaTags,
                    Publisher = item.Publisher,
                    RobotsTxt = item.RobotsTxt,
                    SeoId = item.Id,
                    SiteMap = item.SiteMap
                });
            }

            return result;
        }

        public async Task<ServiceResult> SetDefaultSeo(long seoId)
        {
            try
            {
                if (seoId == 0) return ServiceResult.Error;

                var oldEntity = await _seoRepository.GetQuery().AsQueryable()
                    .Where(s => s.IsDefault).ToListAsync();
                foreach (var oe in oldEntity)
                {
                    oe.IsDefault = false;
                    _seoRepository.EditEntity(oe);
                    await _seoRepository.SaveChanges();
                }

                var entity = await _seoRepository.GetEntityById(seoId);
                if (entity == null) return ServiceResult.NotFond;

                entity.IsDefault = true;

                var result = _seoRepository.EditEntity(entity);
                await _seoRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }
    }
}