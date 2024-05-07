using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Data;
using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Repositories
{
	public class BlogPostCommentRepository : IBlogPostCommentRepository
	{
		private readonly ApplicationDbContext context;

		public BlogPostCommentRepository(ApplicationDbContext context)
        {
			this.context = context;
		}
        public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
		{
			await context.blogPostComments.AddAsync(comment);

			await context.SaveChangesAsync();
			return comment;
		}

		public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
		{
			return await context.blogPostComments.Where(x => x.BlogPostId == blogPostId).
				ToListAsync();

		}
	}
}
