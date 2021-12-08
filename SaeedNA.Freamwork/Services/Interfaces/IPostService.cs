using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Data.Entities.Pages;

namespace SaeedNA.Service.Interfaces
{
    public interface IPostService:IAsyncDisposable
    {
        Task<PostStatusResult> AddNewPost(PostCreateDTO post);
        Task<PostStatusResult> EditPost(PostCreateDTO post);
        Task<PostStatusResult> DeletePost(long postId);
        Task<ICollection<PostCreateDTO>> GetAllPost();
        Task<ICollection<Post>> GetAllDeletedPost();
        Task<ICollection<Post>> GetAllPostDes();
    }
}
