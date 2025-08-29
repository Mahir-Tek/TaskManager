using Microsoft.EntityFrameworkCore;

namespace MainGorevUygulama.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Mission> Missions { get; set; } // Tasks modelini temsil eden DbSet
        public DbSet<User> Users { get; set; } // Users modelini temsil eden DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
