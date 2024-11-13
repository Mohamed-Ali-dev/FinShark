using FinShark.Dtos;
using FinShark.Models;

namespace FinShark.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                lastDiv = stockModel.lastDiv,
                MarketCap = stockModel.MarketCap,
                Industry = stockModel.Industry,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }
        public static Stock ToStockModel(this CreateStockDto createStockDto)
        {
            return new Stock
            {
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Purchase = createStockDto.Purchase,
                lastDiv = createStockDto.lastDiv,
                MarketCap = createStockDto.MarketCap,
                Industry = createStockDto.Industry
            };
        }
    }
}
