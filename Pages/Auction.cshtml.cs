using AuctionApp.Models;
using AuctionApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuctionApp.Pages;

public class AuctionModel : PageModel
{
    private readonly AuctionService _auctionService;

    public AuctionItem? Item { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Email { get; set; } = "";

    public AuctionModel(AuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    public async Task OnGet()
    {
        Item = await _auctionService.GetItem();
    }
}