using FinShark.Data;
using FinShark.Dtos;
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private readonly ApplicationDbContext _db;
        public StockRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Stock> Update(int id, CreateStockDto dto)
        {
            var stockFromDb = await _db.Stocks.FirstOrDefaultAsync(u => u.Id == id);
            stockFromDb.Symbol = dto.Symbol;
            stockFromDb.CompanyName = dto.CompanyName;
            stockFromDb.Purchase = dto.Purchase;
            stockFromDb.lastDiv = dto.lastDiv;
            stockFromDb.MarketCap = dto.MarketCap;
            stockFromDb.Industry = dto.Industry;
            return stockFromDb;
        }
    }
}
