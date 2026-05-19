namespace AuctionApp.Models
{
    public class AuctionItem
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string ImageUrl { get; set; } = "";

        public decimal StartingPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Bid> Bids { get; set; } = new();
    }
}
