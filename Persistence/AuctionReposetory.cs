using Microsoft.EntityFrameworkCore;

namespace DistLab2.Persistence
{
    public class AuctionReposetory : Reposetory<AuctionDb>, IAuctionRepositroy
    {
        public AuctionReposetory(AuctionDbContext context) : base(context)
        {
        }
        public AuctionDb GetById(int id)
        {
            Console.WriteLine("in Overreide method");
            return _dbSet
                .Include(a => a.Bids)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
