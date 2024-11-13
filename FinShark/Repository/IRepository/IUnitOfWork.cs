namespace FinShark.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IStockRepository Stock { get; }
        ICommentRepository Comment { get; }
        Task SaveAsync();

    }
}
