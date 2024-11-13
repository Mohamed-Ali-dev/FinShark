using FinShark.Data;
using FinShark.Dtos;
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
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
