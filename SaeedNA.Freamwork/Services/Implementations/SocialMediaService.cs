using System.Threading.Tasks;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Service.Implementations
{
    public class SocialMediaService : ISocialMediaService
    {
        public readonly IGenericRepository<Data.Models.Settings.SocialMedia> _socialMediaRepository;

        public SocialMediaService(IGenericRepository<Data.Models.Settings.SocialMedia> socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _socialMediaRepository.DisposeAsync();
        }
    }
}