using Microsoft.EntityFrameworkCore;
using TestProject.DAL.Models;

namespace TestProject.DAL.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
