using AuctionApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuctionItem> AuctionItems => Set<AuctionItem>();

        public DbSet<Bid> Bids => Set<Bid>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuctionItem>().HasData(
                new AuctionItem
                {
                    Id = 1,
                    Name = "Vintage Watch",
                    ImageUrl = "/images/watch.jpg",
                    StartingPrice = 50,
                    CurrentPrice = 50,
                    CreatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}
