namespace FinShark.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IStockRepository Stock { get; }
        ICommentRepository Comment { get; }
        IPortfolioRepository Portfolio { get; }
        Task SaveAsync();

    }
}
