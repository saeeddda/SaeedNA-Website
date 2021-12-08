using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Service.Implementations
{
    public class PostService : IPostService
    {
        public readonly IGenericRepository<Post> _postRepository;

        public PostService(IGenericRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _postRepository.DisposeAsync();
        }

        public async Task<ICollection<Post>> GetAllPost()
        {
            return await _postRepository.GetQuery()
                .Include(s=>s.Category)
                .Where(s => !s.IsDelete)
                .ToListAsync();
        }
        public async Task<ICollection<Post>> GetAllDeletedPost()
        {
            return await _postRepository.GetQuery()
                .Include(s=>s.Category)
                .Where(s => s.IsDelete)
                .ToListAsync();
        }

        public async Task<ICollection<Post>> GetAllPostDes()
        {
            return await _postRepository.GetQuery()
                .Include(s=>s.Category)
                .OrderByDescending(s => s.LastUpdateDate)
                .Where(s=>!s.IsDelete)
                .ToListAsync();
        }
    }
}
