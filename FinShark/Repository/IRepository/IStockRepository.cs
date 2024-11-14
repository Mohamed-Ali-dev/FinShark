using FinShark.Dtos;
using FinShark.Models;
using System.Linq.Expressions;

namespace FinShark.Repository.IRepository
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<IEnumerable<Stock>> GetAllAsync(Expression<Func<Stock, bool>>? filter = null,
              Expression<Func<Stock, object>>? orderBy = null, bool? isDescending = false,
              string[]? includeProperties = null, string? companyName = null, string? symbol = null);
        Task<Stock> Update(int id, CreateStockDto dto);
    }
}
