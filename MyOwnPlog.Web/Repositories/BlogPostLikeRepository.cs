using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Data;
using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {

        private readonly ApplicationDbContext context;

        public BlogPostLikeRepository(ApplicationDbContext context)
        {
            this.context = context;   
        }
        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await context.blogPostLikes.AddAsync(blogPostLike);

            await context.SaveChangesAsync();

            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
          return  await context.blogPostLikes.Where(x=>x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid BlogPostId)
        {
          return  await context.blogPostLikes.CountAsync(x=>x.BlogPostId == BlogPostId);
        }
    }
}
