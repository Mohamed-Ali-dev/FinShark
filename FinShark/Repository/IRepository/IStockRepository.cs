using FinShark.Dtos;
using FinShark.Helpers;
using FinShark.Models;
using System.Linq.Expressions;

namespace FinShark.Repository.IRepository
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<IEnumerable<Stock>> GetAllAsync(QueryObject queryObject, Expression<Func<Stock, bool>>? filter = null,
            Expression<Func<Stock, object>>? orderBy = null);
        
        Task<Stock> Update(int id, CreateStockDto dto);
    }
}
