using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Data;
using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {

        private readonly ApplicationDbContext context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await context.AddAsync(blogPost);
           await context.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await context.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                context.BlogPosts.Remove(existingBlog);
              await  context.SaveChangesAsync();

                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await context.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return await context.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == id);

        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string UrlHandle)
        {
           return  await context.BlogPosts.Include(x=>x.Tags)
                .FirstOrDefaultAsync(x=>x.UrlHandle == UrlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
          var existingBlog=  await context.BlogPosts.Include(x=>x.Tags)
                .FirstOrDefaultAsync(x=>x.Id==blogPost.Id);

            if (existingBlog!=null)
            {

                existingBlog.Id = blogPost.Id;
                existingBlog.Tags = blogPost.Tags;
                existingBlog.PublishedDate = blogPost.PublishedDate;

                existingBlog.UrlHandle = blogPost.UrlHandle;

                existingBlog.Visible = blogPost.Visible;

                existingBlog.FeaturedImagedUrl = blogPost.FeaturedImagedUrl;
                existingBlog.Content = blogPost.Content;

                existingBlog.Author = blogPost.Author;
                existingBlog.PageTitle = blogPost.PageTitle;

                existingBlog.ShortDescription = blogPost.ShortDescription;

                existingBlog.Heading = blogPost.Heading;

                await context.SaveChangesAsync();

                return existingBlog;

            }

            return null;

        }
    }
}
