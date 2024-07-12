using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NoteList.DomainLayer.Data
{
    public static class ApplicationDbContextSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            // Seed Roles
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                new IdentityRole<int> { Id = 2, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 3, Name = "User", NormalizedName = "USER" }
            );

            // Seed Admin User
            var superAdminId = 1;
            var hasher = new PasswordHasher<IdentityUser<int>>();
            var adminUser = new IdentityUser<int>
            {
                Id = superAdminId,
                UserName = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "superAdmin@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            builder.Entity<IdentityUser<int>>().HasData(adminUser);

            // Assign Admin Role to Admin User
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                UserId = superAdminId,
                RoleId = 1 // RoleId for Admin
            });
        }
    }
}
