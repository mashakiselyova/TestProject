using Microsoft.EntityFrameworkCore;
using TestProject.DAL.Models;

namespace TestProject.DAL.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRating> PostRatings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentRating> CommentRatings { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<PostRating>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PostRating>()
                .HasOne(r => r.User)
                .WithMany(u => u.PostRatings)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CommentRating>()
                .HasOne(c => c.Comment)
                .WithMany(c => c.CommentRatings)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
