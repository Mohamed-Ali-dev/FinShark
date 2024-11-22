using FinShark.Dtos;
using FinShark.Models;

namespace FinShark.Repository.IRepository
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
       Task<List<Stock>> GetUserPortfolio(AppUser user);
        void Update(Portfolio portfolio);
    }
}
