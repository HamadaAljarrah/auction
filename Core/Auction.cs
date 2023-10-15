namespace DistLab2.Core
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Auctioneer { get; set; }
        public decimal StartingPrice {  get; set; }
        public DateTime AuctionEndDate { get; set; }
        
        //forigen keys från den user som skapade auktionen
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }
}
