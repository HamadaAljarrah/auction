using System.ComponentModel.DataAnnotations;
using DistLab2.Core;

namespace DistLab2.Persistence
{
    public class AuctionDb
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength (128)]
        public string Name { get; set; }


        
        [Required]
        public string Description { get; set; }


        [Required]
        public decimal StartingPrice { get; set; }
       
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

       
        [Required]
        [DataType(DataType.DateTime)] 
        public DateTime EndDate { get; set; }
        
        public virtual IEnumerable<BidDb> Bids{ get; set; }=new List<BidDb>();

    }
}
