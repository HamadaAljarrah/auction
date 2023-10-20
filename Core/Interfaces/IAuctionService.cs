namespace DistLab2.Core.Interfaces
{
    public interface IAuctionService
    {
       public IEnumerable<Auction> GetAll();
        public Auction GetById(int id);

        public void CreateAuction(Auction auction);
       public void Remove(Auction auction);

        public void PlaceBid(int auctionId, int bidAmount);
    }
}
