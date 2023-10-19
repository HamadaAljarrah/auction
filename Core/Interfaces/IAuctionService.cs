namespace DistLab2.Core.Interfaces
{
    public interface IAuctionService
    {
       public IEnumerable<Auction> GetAll();
       public void CreateAuction(Auction auction);
       public void Remove(Auction auction);


    }
}
