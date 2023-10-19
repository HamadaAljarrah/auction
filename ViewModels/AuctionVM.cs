using DistLab2.Core;

namespace DistLab2.ViewModels
{
    public class AuctionVM
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description {get;set;}
        public DateTime CreatedDate { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime EndingDate{get;set;}

        public List<BidVM> Bids = new();
        
        //todo läg in samtlig auctons medlemsvariablar
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
