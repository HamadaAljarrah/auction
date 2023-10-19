namespace DistLab2.Core
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Auctioneer { get; set; }
        public decimal StartingPrice {  get; set; }
        public DateTime EndDate { get; set; }
        
        //forigen keys från den user som skapade auktionen
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Bid> _bids { get; set; }
        public IEnumerable<Bid> Bids => _bids;
        public Auction(int auctionId, string name,  DateTime endDate,string description)
        {
            Id = auctionId;
            Name = name;
            EndDate = endDate;
            Description = description;
        }

    }
}
