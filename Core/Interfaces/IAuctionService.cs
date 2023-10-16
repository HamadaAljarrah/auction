namespace DistLab2.Core.Interfaces
{
    public interface IAuctionService
    {
       public IEnumerable<Auction> GetAll();
       public void CreateAuction(int auctionId, string name, DateTime endDate, string description);
       public void Remove(Auction auction);


    }
}
