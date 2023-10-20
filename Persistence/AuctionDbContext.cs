using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DistLab2.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
        public DbSet<AuctionDb> Auctions { get; set; }
        public DbSet<BidDb> Bids { get; set; }
        public DbSet<UserDb>? Users { get; set; }


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<AuctionDb>()
        //         .HasMany(a => a.Bids)
        //         .WithOne(b => b.Auction)
        //         .HasForeignKey(b => b.AuctionId);

        //     modelBuilder.Entity<BidDb>()
        //         .HasOne(b => b.Auction)
        //         .WithMany(a => a.Bids)
        //         .HasForeignKey(b => b.AuctionId);

 
        // }
    }
}
