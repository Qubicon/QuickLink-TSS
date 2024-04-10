using Microsoft.EntityFrameworkCore;
using QuickLink.Models;

namespace QuickLink.Database
{
    public class QLDbContext : DbContext
    {
        public QLDbContext(DbContextOptions<QLDbContext> options)
            : base(options)
        {
        }

        public DbSet<Url> UrlsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }


}