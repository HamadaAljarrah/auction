namespace DistLab2.Core
{
    public class Bid
    {
        public int BidId {  get; set; }
        public decimal Amount { get; set; } 
        public DateTime BidTime { get; set; }

        //forign key för användren som placeade budet
        public int UserId { get; set; }
        public User User { get; set; }

        //forign key för auctionen
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
    }
}
