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
        private readonly IGenericRepository<Post> _postRepository;

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
            var query = _postRepository.GetQuery().Include(s=>s.Category).AsQueryable();

            query = query.Where(s => s.IsDelete == filter.IsDelete);

            switch (filter.State)
            {
                case PagesPublishState.All:
                    break;
                case PagesPublishState.Draft:
                    query = query.Where(s => s.State == PostPublishingState.Draft);
                    break;
                case PagesPublishState.Published:
                    query = query.Where(s => s.State == PostPublishingState.Published);
                    break;
            }

            if (filter.IsDescending)
                query = query.OrderByDescending(s => s.CreateDate);

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

            if (filter.CategoryId != 0)
                query = query.Where(s => s.CategoryId == filter.CategoryId);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetPost(allEntities).SetPaging(pager);
        }

        public async Task<PostEditDTO> GetPostForEdit(long postId)
        {
            var query = await _postRepository.GetEntityById(postId);
            if (query == null) return null;

            return new PostEditDTO
            {
                Title = query.Title,
                CategoryId = query.CategoryId,
                Image = query.Image,
                State = query.State,
                Description = query.Description,
                ShortDescription = query.ShortDescription,
                Tags = query.Tags,
                Visit = query.Visit,
                PostId = query.Id
            };
        }

        public async Task<PostShowDTO> GetPostShow(long postId)
        {
            var query = await _postRepository.GetQuery()
                .Include(s => s.Category).AsQueryable()
                .SingleOrDefaultAsync(s=>s.Id == postId && !s.IsDelete);

            if(query == null) return null;

            query.Visit += 1;

            _postRepository.EditEntity(query);
            await _postRepository.SaveChanges();

            return new PostShowDTO {
                PostId = query.Id,
                Title = query.Title,
                CategoryName = query.Category.Name,
                Image = query.Image,
                ShortDescription = query.ShortDescription,
                Description = query.Description,
                Tags = query.Tags,
                Visit = query.Visit,
                CreateDate = query.CreateDate,
                LastUpdateDate = query.LastUpdateDate
            };
        }
    }
}
