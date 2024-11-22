using FinShark.Data;
using FinShark.Repository.IRepository;

namespace FinShark.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IStockRepository Stock {  get; private set; }
        public ICommentRepository Comment { get; private set; }
        public IPortfolioRepository Portfolio { get; private set; }

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Stock = new StockRepository(_db);
            Comment = new CommentRepository(_db);
            Portfolio = new PortfolioRepository(_db);
        }


        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}
