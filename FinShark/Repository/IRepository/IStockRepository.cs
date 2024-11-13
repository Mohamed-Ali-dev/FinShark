using FinShark.Dtos;
using FinShark.Models;

namespace FinShark.Repository.IRepository
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<Stock> Update(int id, CreateStockDto dto);
    }
}
