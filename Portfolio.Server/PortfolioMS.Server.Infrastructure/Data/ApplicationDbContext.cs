using Microsoft.EntityFrameworkCore;
using PortfolioMS.Server.Domain.Entities;

namespace PortfolioMS.Server.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<ViewCount> ViewCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.PortfolioMS);

            base.OnModelCreating(modelBuilder);
        }
    }
}
