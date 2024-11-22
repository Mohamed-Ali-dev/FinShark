using FinShark.Data;
using FinShark.Dtos;
using FinShark.Helpers;
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace FinShark.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Comment>> GetAllAsync(CommentQueryObject queryObject, Expression<Func<Comment, bool>>? filter = null,
           Expression<Func<Comment, object>>? orderBy = null)
        {
            IQueryable<Comment> query = _db.Comments;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = queryObject.IsDecsending == true ? query.OrderByDescending(orderBy)
                    : query.OrderBy(orderBy);
            }

            return await query.ToListAsync();

        }
        public async Task<Comment> Update(int commentId, CreateCommentDto dto)
        {
            var commentFromDb = await _db.Comments.FirstOrDefaultAsync(u => u.Id == commentId);
            commentFromDb.Title = dto.Title;
            commentFromDb.Content = dto.Content;
            commentFromDb.StockId = dto.StockId;
            return commentFromDb;
            
        }
    }
}
