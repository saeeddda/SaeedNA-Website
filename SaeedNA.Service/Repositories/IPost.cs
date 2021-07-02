using SaeedNA.Domain.Models.SPost;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface IPost
    {
        //Main functions
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);
        ICollection<Post> GetAllPost(int take = 20);
        Post GetPostById(int id);
        ICollection<Post> GetAllWithCategory();

        //Post functions
        ICollection<Post> GetAllPostDes(int take = 6);
        ICollection<Post> GetAllPostDes(int skip = 0, int take = 6);
        ICollection<Post> GetPostWithCategory();
        Post GetPostByIdWithCategory(int id);
        int GetAllPostCount();
        //Portfolio functions
        ICollection<Post> GetAllPortfolioDes(int take = 6);
        ICollection<Post> GetAllPortfolioDes(int skip = 0, int take = 6);
        ICollection<Post> GetPortfolioWithCategory();
        Post GetPortfolioByIdWithCategory(int id);
        int GetAllPortfolioCount();
    }
}
