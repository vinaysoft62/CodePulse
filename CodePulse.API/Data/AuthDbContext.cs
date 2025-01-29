using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "8ac55222-d705-4369-a125-2cf193963600";
            var writerRoleId = "46d3de7d-47e4-4cad-b15f-9236406ca0fb";

            //create reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },

                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "3cfec31e-2c88-4914-b914-baa4d9b0fda0";
            // Create an Admin User
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",

                NormalizedUserName = "admin@codepulse.com".ToUpper(),
                NormalizedEmail = "admin@codepulse.com".ToUpper(),

            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles to Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId=adminUserId,
                    RoleId = writerRoleId
                }
            };
            
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
