using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using System;
using System.Threading.Tasks;
using SaeedNA.Data.Entities.Pages;

namespace SaeedNA.Service.Interfaces
{
    public interface IPostService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewPost(PostCreateDTO post);
        Task<ServiceResult> EditPost(PostEditDTO post);
        Task<ServiceResult> DeletePost(long postId);
        Task<PostFilterDTO> FilterPost(PostFilterDTO filter);
        Task<PostEditDTO> GetPostForEdit(long postId);
        Task<Post> GetPost(long postId);
    }
}
