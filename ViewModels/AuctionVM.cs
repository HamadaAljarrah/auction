using DistLab2.Core;

namespace DistLab2.ViewModels
{
    public class AuctionVM
    {
        public int AuctionId { get; set; } 
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal StartingPrice { get; set; }
        //todo läg in samtlig auctons medlemsvariablar
        public static AuctionVM FromAuction(Auction auction)
        {
            return new AuctionVM
            {
                AuctionId = auction.AuctionId,
                Name = auction.Name,
                CreatedDate = auction.CreatedDate,
                StartingPrice = auction.StartingPrice,
            };
        }
    }
}
