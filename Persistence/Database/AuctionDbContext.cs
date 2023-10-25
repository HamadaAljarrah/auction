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


    

    }
}
