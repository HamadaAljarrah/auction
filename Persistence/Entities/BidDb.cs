using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DistLab2.Core;

namespace DistLab2.Persistence
{
    public class BidDb
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }



        ////forign key för auctionen
        //[ForeignKey("Auction")]
        //public int AuctionId { get; set; }
        //public virtual AuctionDb Auction { get; set; }


        //// Relation to UsertDb and virtuell for lazyloading
        //[ForeignKey("User")]
        ////public string UserId { get; set; }
        //public UserDb User { get; set; }
        // Foreign key for the auction
     
        [ForeignKey("Auction")]
        public int AuctionId { get; set; }
        public virtual AuctionDb Auction { get; set; }



        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual UserDb User { get; set; }
  
    }

}
