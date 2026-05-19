using AuctionApp.Data;
using AuctionApp.Hubs;
using AuctionApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSignalR();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=auction.db"));

builder.Services.AddScoped<AuctionService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();
}

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.MapHub<AuctionHub>("/auctionHub");

app.MapPost("/api/bid",
    async (
        BidRequest request,
        AuctionService auctionService) =>
    {
        var result = await auctionService.PlaceBid(
            request.Email,
            request.Amount);

        return Results.Json(result);
    });

app.Run();

public record BidRequest(
    string Email,
    decimal Amount);