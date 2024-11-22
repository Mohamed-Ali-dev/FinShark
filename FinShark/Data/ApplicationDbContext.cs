using FinShark.Helpers;
using FinShark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Portfolio>().HasKey(sa => new { sa.StockId, sa.AppUserId });
            modelBuilder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(u => u.AppUserId);
            modelBuilder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios).
                HasForeignKey(u => u.StockId);
            modelBuilder.Entity<Comment>().HasData(comments);

            modelBuilder.Entity<Stock>().HasData(
                new Stock { Id = 1, Symbol = "TSLA", CompanyName = "Tesla", Purchase = 100, lastDiv = 2m, Industry = "Automotive", MarketCap = 234234234 },
                new Stock { Id = 2, Symbol = "MSFT", CompanyName = "Microsoft", Purchase = 100, lastDiv = 1.2m, Industry = "Technology", MarketCap = 2342345 },
                new Stock { Id = 3, Symbol = "VTI", CompanyName = "Vanguard Total Index", Purchase = 200, lastDiv = 2.1m, Industry = "Index Fund", MarketCap = 2342346 },
                new Stock { Id = 4, Symbol = "PLTR", CompanyName = "Plantir", Purchase = 23, lastDiv = 0m, Industry = "Technology", MarketCap = 1234234 }
                );
        }
        List<Comment> comments = new List<Comment>
          {
           new Comment
           {
               Id = 1,
               Title = "Great Stock!",
               Content = "This stock has shown consistent growth over the past year.",
               CreatedOn = DateTime.Now.AddDays(-10),
               StockId = 1,
               AppUserId = "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4"
           },
           new Comment
           {
               Id = 2,
               Title = "High Volatility",
               Content = "Be cautious, as this stock tends to be very volatile.",
               CreatedOn = DateTime.Now.AddDays(-5),
               StockId = 2,
               AppUserId = "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4"

           },
           new Comment
           {
               Id = 3,
               Title = "Solid Investment",
               Content = "Good for long-term investors who are looking for stable returns.",
               CreatedOn = DateTime.Now.AddDays(-3),
               StockId = 1,
              AppUserId = "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4" 
           },
           new Comment
           {
               Id = 4,
               Title = "Undervalued",
               Content = "I believe this stock is currently undervalued and could be a good buy.",
               CreatedOn = DateTime.Now.AddDays(-1),
               StockId = 3,
               AppUserId = "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4"
           },
           new Comment
           {
               Id = 5,
               Title = "Overpriced",
               Content = "This stock is trading at a high price-to-earnings ratio.",
               CreatedOn = DateTime.Now,
               StockId = 4,
           AppUserId = "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4"
          }
        };
    }

}
