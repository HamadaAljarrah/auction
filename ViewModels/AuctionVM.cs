using DistLab2.Core;
using DistLab2.Persistence;

namespace DistLab2.ViewModels
{
    public class AuctionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingPrice { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserId { get; set; }

        public List<BidVM> Bids = new();
        //public virtual IEnumerable<BidVM> Bids { get; set; } = new List<BidVM>();

        public byte[] Image { get; set; }
        public static AuctionVM FromAuction(Auction auction)
        {
            return new AuctionVM
            {
                Id = auction.Id,
                Name = auction.Name,
                CreatedDate = auction.EndDate,
                StartingPrice = auction.StartingPrice,
            };
        }
    }
}
