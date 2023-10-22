namespace DistLab2.Core.Interfaces
{
    public interface IAuctionService
    {

        IEnumerable<Auction> GetAll();
        void CreateAuction(Auction auction);
        Auction GetById(int id);
        void UpdateDescription(string description, int id);
        void DeleteAuction(int id);

        void PlaceBid(Bid bid);


        public Auction GetAuctionWithBids(int id);
        IEnumerable<Auction> GetUserAuctions(string userId);
        IEnumerable<Auction> GetUnownedAuctions(string userId);

        IEnumerable<Auction> GetAuctionsUserBidIn(string userId);
        IEnumerable<Auction> GetAuctionsUserWon(string userId);


    }
}
