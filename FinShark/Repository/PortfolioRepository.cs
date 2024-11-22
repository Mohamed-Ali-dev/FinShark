using FinShark.Data;
using FinShark.Dtos;
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        private readonly ApplicationDbContext _db;
        public PortfolioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _db.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(s => new Stock
                {
                    Id = s.StockId,
                    Symbol = s.Stock.Symbol,
                    CompanyName = s.Stock.CompanyName,
                    Purchase = s.Stock.Purchase,
                    lastDiv = s.Stock.lastDiv,
                    Industry = s.Stock.Industry,
                    MarketCap = s.Stock.MarketCap,
                }).ToListAsync();
        }

        public void Update(Portfolio portfolio)
        {
         _db.Update(portfolio);
        }
    }
}
