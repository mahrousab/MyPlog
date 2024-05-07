using Azure;
using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Data;
using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext context;

        public TagRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await context.Tags.AddAsync(tag);
            await context.SaveChangesAsync();
            return (tag);
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await context.Tags.FindAsync(id);
            if(existingTag  != null)
            {
                context.Tags.Remove(existingTag);
                await context.SaveChangesAsync();

                return existingTag;


            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return  await context.Tags.ToListAsync();
        }

        public  Task<Tag?> GetAsync(Guid id)
        {
           return context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await context.Tags.FindAsync(tag.Id);
            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await context.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
