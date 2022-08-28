using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface ISeoService : IAsyncDisposable
    {
        Task<ServiceResult> AddNewSeo(SeoCreateDTO seo);
        Task<ServiceResult> EditSeo(SeoGetSetDTO seo);
        Task<ServiceResult> DeleteSeo(long seoId);
        Task<SeoFilterDTO> FilterSeo(SeoFilterDTO filter);
        Task<SeoGetSetDTO> GetSeoById(long seoId);
        Task<ServiceResult> SetDefaultSeo(long seoId);
    }
}