using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {

        Task<int> GetTotalLikes(Guid BlogPostId);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
