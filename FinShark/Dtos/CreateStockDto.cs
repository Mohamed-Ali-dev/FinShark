using System.ComponentModel.DataAnnotations;

namespace FinShark.Dtos
{
    public class CreateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "symbol cannot be over 20 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal lastDiv { get; set; }
        [Required]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 5000000000)]

        public long MarketCap { get; set; }
    }
}
