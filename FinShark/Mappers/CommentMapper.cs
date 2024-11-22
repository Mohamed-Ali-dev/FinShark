using FinShark.Dtos;
using FinShark.Models;

namespace FinShark.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = commentModel.AppUser?.FullName ?? "Anonymous",
                StockId = commentModel.StockId
            };
        }
        public static Comment ToCommentModel(this CreateCommentDto dto)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                StockId = dto.StockId
            };
        }
    }
}
