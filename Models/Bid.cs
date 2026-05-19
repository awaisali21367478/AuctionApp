namespace AuctionApp.Models
{
    public class Bid
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string UserEmail { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public int AuctionItemId { get; set; }

        public AuctionItem AuctionItem { get; set; } = null!;
    }
}
