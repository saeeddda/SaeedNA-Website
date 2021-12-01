using SaeedNA.Service.Repositories;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.SPost;
using System.Collections.Generic;
using System.Linq;
using SaeedNA.Service.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SaeedNA.Service.Services
{
    public class PostService : IPost
    {
        #region Ctor

        public readonly SaeedNAContext _context;

        public PostService(SaeedNAContext context)
        {
            _context = context;
        }

        #endregion

        #region Main Function

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.PostId == id);
        }

        public ICollection<Post> GetAllPost(int take = 20)
        {
            return _context.Posts
                .Take(take)
                .ToList();
        }

        public ICollection<Post> GetAllWithCategory()
        {
            return _context.Posts
                .Include(c => c.Category)
                .ToList();
        }

        #endregion

        #region Post

        public int GetAllPostCount()
        {
            return _context.Posts
                .Where(p => p.PostType == "article")
                .Count();
        }

        public ICollection<Post> GetAllPostDes(int take = 6)
        {
            return _context.Posts
                .Where(p => p.PostType == "article")
                .OrderByDescending(p => p.PostId)
                .OrderByDescending(p => p.PostCreateDate)
                .Take(take)
                .ToList();
        }

        public ICollection<Post> GetAllPostDes(int skip = 0, int take = 6)
        {
            return _context.Posts
                .Where(p => p.PostType == "article")
                .OrderByDescending(p => p.PostCreateDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Post GetPostByIdWithCategory(int id)
        {
            return _context.Posts
                .Where(p => p.PostId == id && p.PostType == "article")
                .Include(c => c.Category)
                .SingleOrDefault();
        }

        public ICollection<Post> GetPostWithCategory()
        {
            return _context.Posts
                .Where(p => p.PostType == "article")
                .Include(c => c.Category)
                .ToList();
        }

        #endregion

        #region Portfolio

        public int GetAllPortfolioCount()
        {
            return _context.Posts
                .Where(p => p.PostType == "project")
                .Count();
        }

        public ICollection<Post> GetAllPortfolioDes(int take = 6)
        {
            return _context.Posts
                .Where(p => p.PostType == "project")
                .OrderByDescending(p => p.PostId)
                .OrderByDescending(p => p.PostCreateDate)
                .Take(take)
                .ToList();
        }

        public ICollection<Post> GetAllPortfolioDes(int skip = 0, int take = 6)
        {
            return _context.Posts
                .Where(p => p.PostType == "project")
                .OrderByDescending(p => p.PostCreateDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Post GetPortfolioByIdWithCategory(int id)
        {
            return _context.Posts
                .Where(p => p.PostId == id && p.PostType == "project")
                .Include(c => c.Category)
                .SingleOrDefault();
        }

        public ICollection<Post> GetPortfolioWithCategory()
        {
            return _context.Posts
                .Where(p => p.PostType == "project")
                .Include(c => c.Category)
                .ToList();
        }

        #endregion
    }
}
