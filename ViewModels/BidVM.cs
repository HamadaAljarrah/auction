

namespace DistLab2.ViewModels
{
    public class BidVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedTime { get; set; }
         public int AuctionId { get; set; }
        public string UserId { get; set; }

        public BidVM(){}

    }
}