namespace DistLab2.Core.Interfaces
{
    public interface IAuctionService
    {
       public IEnumerable<Auction> GetAll();
       public void CreateAuction(int auctionId, string name, DateTime createdDate, DateTime endDate, string description, decimal statingPrice);
       public void Remove(Auction auction);


    }
}
