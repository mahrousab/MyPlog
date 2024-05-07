using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MyOwnPlog.Web.Data
{
    public class AuthDbContext  : IdentityDbContext
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { 
        
        
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // seed Roles (Admin, SuperAdmin, User)
            var AdminRoleId = "b6660726-9a3c-4e3f-b17a-a1c36f4011a3";

            var SuperAdminRoleId = "3706e42c-8bd9-4d1d-8bf6-49b2ffaf87d6";

            var UserRole = "dcff1fff-5f96-49a6-b34d-abdd6806706e";

            
            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId
                },

               new IdentityRole
               {
                   Name ="SuperAdmin",
                   NormalizedName = "SuperAdmin",
                   Id = SuperAdminRoleId,
                   ConcurrencyStamp = SuperAdminRoleId
               },

               new IdentityRole
               {
                   Name = "User",
                   NormalizedName = "User",
                   Id = UserRole,
                   ConcurrencyStamp = UserRole
               }
                    
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // seed SuperAdminUser

            var SuperAdminId = "0b690e53-213d-4597-a131-cc9a29b6bfe7";
            var superAdminUser = new IdentityUser
            {
                UserName = "Mahrous@gmail.com",
                Email = "mahrous@Gmail.com",
                NormalizedEmail = "mahrousGmail.com".ToUpper(),
                NormalizedUserName = "mahrousGmail.com".ToUpper(),
                Id = SuperAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "mahrousEllithy123");
            
            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // Add All Roles To SuperAdmin

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = AdminRoleId,
                    UserId = SuperAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = SuperAdminRoleId,
                    UserId = SuperAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId =UserRole ,
                    UserId = SuperAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }

    }
}
