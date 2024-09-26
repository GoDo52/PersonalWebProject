using Microsoft.EntityFrameworkCore;
using PersonalWeb_Razor.Models;

namespace PersonalWeb_Razor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Convenience" },
                new Category { Id = 3, Name = "Medicine" },
                new Category { Id = 4, Name = "Restaurant" },
                new Category { Id = 5, Name = "Other" }
                );
        }
    }
}
