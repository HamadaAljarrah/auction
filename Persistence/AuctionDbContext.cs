using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DistLab2.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
        public DbSet<AuctionDb> AuctionDbs { get; set; }//todo läg till all som ska setas så db set bidDb osv

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionDb>().HasData(
                new AuctionDb
                {
                    Id = -1,
                    Name = "TEST from aucton db dontext",
                    CreatedDate = DateTime.Now,
                    
                });
        }
    }
}
