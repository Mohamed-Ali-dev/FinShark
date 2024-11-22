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
        public async Task<IEnumerable<Stock>> GetAllAsync(QueryObject queryObject, Expression<Func<Stock, bool>>? filter = null,
            Expression<Func<Stock, object>>? orderBy = null)
        
        {
            IQueryable<Stock> query = _db.Stocks.Include(s => s.Comments).ThenInclude(s => s.AppUser);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(queryObject.CompanyName))
            {
               query = _db.Stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }
            else if (!string.IsNullOrEmpty(queryObject.Symbol))
            {
                query = _db.Stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));

            }
            if (orderBy!= null)
            {
                query = queryObject.isDescending == true ? query.OrderByDescending(orderBy)
                    : query.OrderBy(orderBy);
            }
            var skipNumber = (queryObject.pageNumber - 1) * queryObject.pageSize;
            
            return await query.Skip(skipNumber).Take(queryObject.pageSize).ToListAsync();

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
