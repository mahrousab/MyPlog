using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Repositories
{
	public interface IBlogPostCommentRepository
	{

		 Task<BlogPostComment> AddAsync(BlogPostComment comment);	

		Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
	}
}
