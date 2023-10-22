using AutoMapper.Internal.Mappers;

namespace DistLab2.Core
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingPrice {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        
        //forigen keys från den user som skapade auktionen
        public string UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<Bid> Bids { get; set; } = new List<Bid>();

        public byte[] Image { get; set; }
        public Auction(){}

        public override string ToString()
        {
            return $"Auction Id: {Id}, Name: {Name}, Description: {Description}, " +
                   $"Starting Price: {StartingPrice}, Created Date: {CreatedDate}, " +
                   $"End Date: {EndDate}, User Id: {UserId}";
        }

    }

}
