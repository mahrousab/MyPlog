using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Repositories
{
    public interface IBlogPostRepository
    {

        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string UrlHandle);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid Id);
    }
}
