using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.Models.Settings;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class SeoService:ISeoService
    {
        public readonly IGenericRepository<Seo> _seoRepository;

        public SeoService(IGenericRepository<Seo> seoRepository)
        {
            _seoRepository = seoRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _seoRepository.DisposeAsync();
        }

        public async Task<Seo> GetDefaultSeo()
        {
            return await _seoRepository.GetQuery().SingleOrDefaultAsync(s => s.IsDefault);
        }
    }
}