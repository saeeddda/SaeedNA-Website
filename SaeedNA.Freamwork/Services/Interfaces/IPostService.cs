using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface IPostService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewPost(PostCreateDTO post);
        Task<ServiceResult> EditPost(PostEditDTO post);
        Task<ServiceResult> DeletePost(long postId);
        Task<PostFilterDTO> FilterPost(PostFilterDTO filter);
    }
}
