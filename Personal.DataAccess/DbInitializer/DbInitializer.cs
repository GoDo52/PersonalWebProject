using Microsoft.EntityFrameworkCore;
using Personal.DataAccess.Data;
using Personal.Models;
using Personal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;

        public DbInitializer (ApplicationDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            // migrations if not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            // create roles if they are not created
            if (!_db.Roles.Any(r => r.Name == SD.AdminRole))
            {
                _db.Roles.AddRange(
                    new Role { Name = SD.AdminRole },
                    new Role { Name = SD.ManagerRole },
                    new Role { Name = SD.UserRole }
                );
                _db.SaveChanges();

                // create Admin User
                if (!_db.Users.Any(u => u.UserName == "admin"))
                {
                    PasswordHasher hasher = new PasswordHasher();
                    var passwordHash = hasher.HashPassword("Admin123!");

                    _db.Users.Add(new User
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        PasswordHash = passwordHash.Hash,
                        PasswordSalt = passwordHash.Salt,
                        RoleId = _db.Roles.FirstOrDefault(r => r.Name == "Admin")?.Id ?? 0
                    });

                    _db.SaveChanges();
                }
            }
        }
    }
}
