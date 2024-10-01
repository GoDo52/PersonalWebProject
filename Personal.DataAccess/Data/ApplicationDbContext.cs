using Microsoft.EntityFrameworkCore;
using Personal.Models;

namespace Personal.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Spending> Spendings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Convenience" },
                new Category { Id = 3, Name = "Medicine" },
                new Category { Id = 4, Name = "Restaurant" },
                new Category { Id = 5, Name = "Other" }
            );

            modelBuilder.Entity<Spending>().ToTable("Spendings");
            modelBuilder.Entity<Spending>().HasData(
                new Spending { Id = 1, Amount=10, CategoryId=1, DateTime=DateTime.Now, UserId=1 },
                new Spending { Id = 2, Amount=19, CategoryId=4, DateTime=DateTime.Now, UserId=2, Description = null },
                new Spending { Id = 3, Amount=5, CategoryId=2, DateTime=DateTime.Now, UserId=1, Description = null }
            );
        }
    }
}
