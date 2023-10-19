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
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Bid> _bids { get; set; }

    }
}
