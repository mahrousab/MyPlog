using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Data
{
	public class ApplicationDbContext : DbContext
	{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogPostLike> blogPostLikes { get; set; }

        public DbSet<BlogPostComment> blogPostComments { get; set; }
    }
}
