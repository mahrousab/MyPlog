using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Data;

namespace MyOwnPlog.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var superAdmin = await authDbContext.Users.
                FirstOrDefaultAsync(x=>x.Email == "admin");

            if(superAdmin != null)
            {
                users.Remove(superAdmin);
            }

            return users;
        }
    }

}


    
