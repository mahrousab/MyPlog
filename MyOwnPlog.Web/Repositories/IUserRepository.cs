using Microsoft.AspNetCore.Identity;

namespace MyOwnPlog.Web.Repositories
{
    public interface IUserRepository
    {

        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
