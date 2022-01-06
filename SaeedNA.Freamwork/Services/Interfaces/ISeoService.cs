using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.Entities.Settings;

namespace SaeedNA.Service.Interfaces
{
    public interface ISeoService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewSeo(SeoCreateDTO seo);
        Task<ServiceResult> EditSeo(SeoEditDTO seo);
        Task<ServiceResult> DeleteSeo(long seoId);
        Task<SeoFilterDTO> FilterSeo(SeoFilterDTO filter);
        Task<Seo> GetDefaultSeo();
    }
}