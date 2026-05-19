# AuctionApp

# Vintage Watch Auction

A real-time auction web application built with ASP.NET Core 9, Razor Pages, SignalR, Entity Framework Core, and SQLite.

Users can join the auction using only their email address, place bids in real-time, and instantly see updates across multiple browsers without refreshing the page.

# Live Demo

https://auctionapp.runasp.net/

# Features

- Real-time bidding using SignalR
- Live highest bid updates across all connected clients
- Bid validation rules
- Minimum bid increment of $5
- Last 5 bid history
- Simple email-based login
- SQLite database persistence
- Responsive modern UI
- Concurrency-safe bid processing

# Tech Stack

- ASP.NET Core 9
- Razor Pages
- SignalR
- Entity Framework Core
- SQLite
- Bootstrap
- JavaScript

# Project Structure

AuctionApp/
│
├── Data/
├── Hubs/
├── Models/
├── Pages/
├── Services/
├── wwwroot/
│
└── Program.cs

# How to Run Locally

## 1. Clone Repository

## 2. Navigate to Project

## 3. Restore Packages

## 4. Apply Database Migration

## 5. Run Application

# Real-Time Functionality

The application uses SignalR for real-time communication.

When a user places a bid:

1. Bid is validated server-side
2. Bid is saved to SQLite database
3. SignalR broadcasts a `BidUpdated` event
4. All connected clients receive the update instantly

# Bid Validation Rules

- Bid must be higher than current highest bid
- Minimum increment is $5
- Validation occurs server-side to prevent manipulation

# Concurrency Handling

To prevent race conditions when multiple users bid at the same time, the application uses:

```csharp
SemaphoreSlim
```

This ensures only one bid is processed at a time.

# Deployment

The application is deployed on:

- RunASP.NET

# Author

Your Name
