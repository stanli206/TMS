using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.Users.Any()) return;

            db.Users.AddRange(
                new User(
                    "admin@test.com",
                    BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    "Admin"
                ),
                new User(
                    "user@test.com",
                    BCrypt.Net.BCrypt.HashPassword("User@123"),
                    "User"
                ),
                new User(
                    "auditor@test.com",
                    BCrypt.Net.BCrypt.HashPassword("Audit@123"),
                    "Auditor"
                )
            );

            await db.SaveChangesAsync();
        }
    }
}
