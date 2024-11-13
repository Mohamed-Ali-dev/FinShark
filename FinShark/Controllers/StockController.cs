using FinShark.Dtos;
using FinShark.Mappers;
using FinShark.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StockController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stocks = await _unitOfWork.Stock.GetAllAsync(includeProperties: new[] {"Comments"});
            var stokDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocks);
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
            var stock = await _unitOfWork.Stock.GetAsync(s => s.Id == id,"Comments");

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
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

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
            return Ok(stockFromDb.ToStockDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!_unitOfWork.Stock.ObjectExistAsync(u => u.Id == id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            var stockToDelete = await _unitOfWork.Stock.GetAsync(u => u.Id == id);
            _unitOfWork.Stock.Delete(stockToDelete);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
