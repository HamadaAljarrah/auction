namespace DistLab2.Core
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal StartingPrice {  get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime EndDate { get; set; }
        
        //forigen keys från den user som skapade auktionen
        public int UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<Bid> _bids { get; set; }
        public IEnumerable<Bid> Bids => _bids;
        public Auction(){}

    }
}
