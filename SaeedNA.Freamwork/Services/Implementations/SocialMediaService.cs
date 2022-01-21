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
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IGenericRepository<SocialMedia> _socialMediaRepository;

        public SocialMediaService(IGenericRepository<SocialMedia> socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _socialMediaRepository.DisposeAsync();
        }

        public async Task<ServiceResult> AddNewSocialMedia(SocialMediaCreateDTO socialMedia)
        {
            try
            {
                var entity = new SocialMedia
                {
                    MediaName = socialMedia.MediaName,
                    MediaIcon = socialMedia.MediaIcon,
                    MediaLink = socialMedia.MediaLink
                };

                var result = await _socialMediaRepository.AddEntity(entity);
                await _socialMediaRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditSocialMedia(SocialMediaEditDTO socialMedia)
        {
            try
            {
                var entity = await _socialMediaRepository.GetEntityById(socialMedia.SocialMediaId);
                if (entity == null) return ServiceResult.NotFond;

                entity.MediaName = socialMedia.MediaName;
                entity.MediaIcon = socialMedia.MediaIcon;
                entity.MediaLink = socialMedia.MediaLink;

                var result = _socialMediaRepository.EditEntity(entity);
                await _socialMediaRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteSocialMedia(long socialMediaId)
        {
            try
            {
                var entity = await _socialMediaRepository.GetEntityById(socialMediaId);
                if (entity == null) return ServiceResult.NotFond;

                var result = _socialMediaRepository.DeleteEntity(entity);
                await _socialMediaRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<SocialMediaFilterDTO> FilterSocialMedia(SocialMediaFilterDTO filter)
        {
            var query = _socialMediaRepository.GetQuery().AsQueryable();

            query = query.Where(s => s.IsDelete == filter.IsDelete);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetSocialMedia(allEntities).SetPaging(pager);
        }

        public async Task<SocialMediaEditDTO> GetSocialMediaForEdit(long socialMediaId)
        {
            var query = await _socialMediaRepository.GetEntityById(socialMediaId);

            if (query == null) return null;

            return new SocialMediaEditDTO
            {
                SocialMediaId = query.Id,
                MediaIcon = query.MediaIcon,
                MediaName = query.MediaName,
                MediaLink   = query.MediaLink
            };
        }
    }
}