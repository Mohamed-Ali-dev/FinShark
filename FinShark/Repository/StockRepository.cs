using FinShark.Data;
using FinShark.Dtos;
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using FinShark.Helpers;

namespace FinShark.Repository
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private readonly ApplicationDbContext _db;
        public StockRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Stock>> GetAllAsync(Expression<Func<Stock, bool>>? filter = null,
            Expression<Func<Stock, object>>? orderBy = null, bool? isDescending = false,
            string[]? includeProperties = null, string? companyName = null, string? symbol = null)
        
        {
            IQueryable<Stock> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(companyName))
            {
               query = _db.Stocks.Where(s => s.CompanyName.Contains(companyName));
            }
            else if (!string.IsNullOrEmpty(symbol))
            {
                query = _db.Stocks.Where(s => s.Symbol.Contains(symbol));

            }
            if (orderBy!= null)
            {
                query = isDescending == true ? query.OrderByDescending(orderBy)
                    : query.OrderBy(orderBy);
            }
            if (includeProperties != null)
            {
                foreach (var includeprop in includeProperties)
                {
                    query = query.Include(includeprop);
                }
            }
            return await query.ToListAsync();

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
