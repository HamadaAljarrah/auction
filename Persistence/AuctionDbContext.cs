using System.Reflection;
using DistLab2.Core;
using Microsoft.EntityFrameworkCore;

namespace DistLab2.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
        public DbSet<AuctionDb> Auctions { get; set; }
        public DbSet<BidDb> Bids { get; set; }
        public DbSet<UserDb>? Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BidDb>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bids)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull); // Remove cascading action


            //modelBuilder.Entity<BidDb>()
            //.HasOne(b => b.Auction)
            //.WithMany(a => a.Bids)
            //.HasForeignKey(b => b.AuctionId)
            //.OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<BidDb>()
            //    .HasOne(b => b.User)
            //    .WithMany(u => u.Bids)
            //    .HasForeignKey(b => b.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
