using FinShark.Data;
using FinShark.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinShark.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? filter = null,
            Expression<Func<T,object>>? orderBy = null, bool? isDescending = false,  string[]? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query =  query.Where(filter);
            }
            if (orderBy != null)
            {
                query = isDescending == true ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
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

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbset;
            }
            else
            {
                query = dbset.AsNoTracking();
            }
            query = query.Where(filter);

            if (includeProperties != null )
            {
                foreach (var includeprop in includeProperties)
                {
                    query = query.Include(includeprop);
                }
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<bool> ObjectExistAsync(Expression<Func<T, bool>> filter)
        {
            return await dbset.AnyAsync(filter);
        }

        public async Task CreateAsync(T entity)
        {
           await _db.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        
    }
}
