using AuctionApp.Data;
using AuctionApp.Hubs;
using AuctionApp.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Services;

public class AuctionService
{
    private readonly ApplicationDbContext _context;

    private readonly IHubContext<AuctionHub> _hub;

    private static readonly SemaphoreSlim _lock = new(1, 1);

    public AuctionService(
        ApplicationDbContext context,
        IHubContext<AuctionHub> hub)
    {
        _context = context;
        _hub = hub;
    }

    public async Task<AuctionItem?> GetItem()
    {
        return await _context.AuctionItems
            .Include(x => x.Bids.OrderByDescending(b => b.CreatedAt))
            .FirstOrDefaultAsync();
    }

    public async Task<BidResponse> PlaceBid(
        string email,
        decimal amount)
    {
        await _lock.WaitAsync();

        try
        {
            var item = await _context.AuctionItems
                .Include(x => x.Bids)
                .FirstOrDefaultAsync();

            if (item == null)
                return new BidResponse
                {
                    Success = false,
                    Message = "Auction item not found"
                };

            if (amount <= item.CurrentPrice)
                return new BidResponse
                {
                    Success = false,
                    Message = "Bid must be higher than current bid"
                };

            if (amount < item.CurrentPrice + 5)
                return new BidResponse
                {
                    Success = false,
                    Message = "Minimum increment is $5"
                };

            var bid = new Bid
            {
                Amount = amount,
                UserEmail = email,
                AuctionItemId = item.Id,
                CreatedAt = DateTime.UtcNow
            };

            item.CurrentPrice = amount;

            _context.Bids.Add(bid);

            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("BidUpdated");

            return new BidResponse
            {
                Success = true,
                Message = "Bid placed successfully"
            };
        }
        finally
        {
            _lock.Release();
        }
    }
}