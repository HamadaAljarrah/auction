using System.ComponentModel.DataAnnotations;


namespace DistLab2.Persistence
{
    public class UserDb
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }


        // Virtuel navigation property for lazyloading
        public virtual List<AuctionDb> Auctions { get; set; } = new();
        public virtual List<BidDb> Bids { get; set; } = new();


        public UserDb() { }
    }
}