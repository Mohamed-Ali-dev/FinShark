using FinShark.Dtos;
using FinShark.Helpers;
using FinShark.Mappers;
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StockController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            IEnumerable<Stock> stocks;
            if(string.Equals(query.orderBy, "symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = await _unitOfWork.Stock.GetAllAsync( query
                , orderBy: u => u.Symbol);
            }
            else if(string.Equals(query.orderBy, "companyName", StringComparison.OrdinalIgnoreCase))
            {
                stocks = await _unitOfWork.Stock.GetAllAsync(query
                 , orderBy: u => u.CompanyName);
            }
            else
            {
                stocks = await _unitOfWork.Stock.GetAllAsync(query);
            }

            if (!stocks.Any())
            {
                return NotFound("No stocks found matching the specified criteria.");
            }

            var stokDto = stocks.Select(s => s.ToStockDto());
            return Ok(stokDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!_unitOfWork.Stock.ObjectExistAsync(u => u .Id == id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var stock = await _unitOfWork.Stock.GetAsync(s => s.Id == id, new[] { "Comments" });

            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var stockModel = dto.ToStockModel();
            await _unitOfWork.Stock.CreateAsync(stockModel);
            await _unitOfWork.SaveAsync();
            var createdStock = await _unitOfWork.Stock.GetAsync(s => s.Id == stockModel.Id, new[] { "Comments" });
            return CreatedAtAction(nameof(GetById), new { id = createdStock.Id }, createdStock.ToStockDto());

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateStockDto dto)
        {
            if(!_unitOfWork.Stock.ObjectExistAsync(U => U.Id == id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
        
          var stockFromDb =  await _unitOfWork.Stock.Update(id, dto);
           await _unitOfWork.SaveAsync();
            var updatedStock = await _unitOfWork.Stock.GetAsync(s => s.Id == stockFromDb.Id, new[] { "Comments" });

            return Ok(updatedStock.ToStockDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!_unitOfWork.Stock.ObjectExistAsync(u => u.Id == id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var stockToDelete = await _unitOfWork.Stock.GetAsync(u => u.Id == id);
            _unitOfWork.Stock.Delete(stockToDelete);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
