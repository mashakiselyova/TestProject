using Microsoft.EntityFrameworkCore;
using TestProject.DAL.Models;

namespace TestProject.DAL.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
