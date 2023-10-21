namespace DistLab2.Persistence
{
    public interface IAuctionRepositroy : IReposetory<AuctionDb>
    {
        public AuctionDb GetById(int id);
    }
}
