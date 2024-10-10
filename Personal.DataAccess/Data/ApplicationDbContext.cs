using Microsoft.EntityFrameworkCore;
using Personal.Models;
using Personal.Utility;

namespace Personal.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Convenience" },
                new Category { Id = 3, Name = "Medicine" },
                new Category { Id = 4, Name = "Restaurant" },
                new Category { Id = 5, Name = "Other" }
            );

            modelBuilder.Entity<Spending>().ToTable("Spendings");
            //modelBuilder.Entity<Spending>().HasData(
            //    new Spending { Id = 1, Amount=10, CategoryId=1, DateTime=DateTime.Now, UserId=1 },
            //    new Spending { Id = 2, Amount=19, CategoryId=4, DateTime=DateTime.Now, UserId=2, Description = null },
            //    new Spending { Id = 3, Amount=5, CategoryId=2, DateTime=DateTime.Now, UserId=1, Description = null }
            //);

            modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, Email = "alex@gmail.com", Password = "12345678", RoleId = 1, UserName = "Alex"}
            //);

            modelBuilder.Entity<Role>().ToTable("Roles");
            for ( int i = 1; i <= SD.RoleNames.Length; i++)
            {
                modelBuilder.Entity<Role>().HasData(
                    new Role { Id = i, Name = SD.RoleNames[i - 1] }
                );
            }
        }
    }
}
