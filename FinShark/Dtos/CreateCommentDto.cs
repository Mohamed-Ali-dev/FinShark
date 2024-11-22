using System.ComponentModel.DataAnnotations;

namespace FinShark.Dtos
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage ="Title must be 5 characters at least")]
        [MaxLength(280, ErrorMessage ="Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "content must be 5 characters at least")]
        [MaxLength(280, ErrorMessage = "content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
        public int? StockId { get; set; }
    }
}
