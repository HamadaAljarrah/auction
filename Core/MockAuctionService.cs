using DistLab2.Core.Interfaces;

namespace DistLab2.Core
{
    public class MockAuctionService : IAuctionService
    {
        public void CreateAuction(int auctionId, string name, DateTime endDate, string description)
        {
            throw new NotImplementedException();
        }

        //todo remove hela klassen här är ett hård kodad aucton för att testa controller
        public List<Auction> GetAll()
        {
            Auction a1 = new Auction(1, "first auction", DateTime.Now,"");
            Auction a2 = new Auction(2, "second auction", DateTime.Now,"");
            List<Auction> auctions = new();
            auctions.Add(a1);
            auctions.Add(a2);  
            return auctions;
        }

        public void Remove(Auction auction)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Auction> IAuctionService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
