using FinShark.Dtos;
using FinShark.Models;

namespace FinShark.Repository.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> Update(int commentId, CreateCommentDto dto);
    }
}
