using System.Linq.Expressions;

namespace FinShark.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null, bool? isDescending = false, string[]? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null, bool tracked = false);
        Task<bool> ObjectExistAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
