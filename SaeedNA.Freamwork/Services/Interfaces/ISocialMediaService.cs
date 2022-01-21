using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Site;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface ISocialMediaService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewSocialMedia(SocialMediaCreateDTO socialMedia);
        Task<ServiceResult> EditSocialMedia(SocialMediaEditDTO socialMedia);
        Task<ServiceResult> DeleteSocialMedia(long socialMediaId);
        Task<SocialMediaFilterDTO> FilterSocialMedia(SocialMediaFilterDTO filter);
        Task<SocialMediaEditDTO> GetSocialMediaForEdit(long socialMediaId);    
    }
}