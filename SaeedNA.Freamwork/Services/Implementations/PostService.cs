using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ServiceResult> AddNewPost(PostCreateDTO post)
        {
            try
            {
                var entity = new Post
                {
                    Title = post.Title,
                    ShortDescription = post.ShortDescription,
                    Description = post.Description,
                    CategoryId = post.CategoryId,
                    Image = post.Image,
                    State = post.State,
                    Tags = post.Tags,
                    Visit = 0
                };

                var result = await _postRepository.AddEntity(entity);
                await _postRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditPost(PostEditDTO post)
        {
            try
            {
                var entity = await _postRepository.GetEntityById(post.PostId);
                if(entity == null) return ServiceResult.NotFond;

                entity.Title = post.Title;
                entity.ShortDescription = post.ShortDescription;
                entity.Description = post.Description;
                entity.CategoryId = post.CategoryId;
                entity.Image = post.Image;
                entity.State = post.State;
                entity.Tags = post.Tags;

                var result = _postRepository.EditEntity(entity);
                await _postRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeletePost(long postId)
        {
            try
            {
                var entity = await _postRepository.GetEntityById(postId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _postRepository.DeleteEntity(entity);
                await _postRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<PostFilterDTO> FilterPost(PostFilterDTO filter)
        {
            var query = _postRepository.GetQuery().AsQueryable();

            switch (filter.State)
            {
                case PagesPublishState.All:
                    query = query.Where(s =>  s.IsDelete == filter.IsDelete);
                    break;
                case PagesPublishState.Draft:
                    query = query.Where(s => s.State == PostPublishingState.Draft && s.IsDelete == filter.IsDelete);
                    break;
                case PagesPublishState.Published:
                    query = query.Where(s => s.State == PostPublishingState.Published && s.IsDelete == filter.IsDelete);
                    break;
            }

            if (filter.IsDescending)
                query = query.OrderByDescending(s => s.CreateDate);

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

            if (filter.CategoryId >= 0)
                query = query.Where(s => s.CategoryId == filter.CategoryId);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetPost(allEntities).SetPaging(pager);
        }
    }
}
