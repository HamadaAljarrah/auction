using System.ComponentModel.DataAnnotations;
using DistLab2.Core;

namespace DistLab2.Persistence
{
    public class BidDb
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedTime { get; set; }

        //forign key för användren som placeade budet
        public int UserId { get; set; }
        public User User { get; set; }

        //forign key för auctionen
        public int AuctionId { get; set; }
        public AuctionDb AuctionDb { get; set; }
    }

}
