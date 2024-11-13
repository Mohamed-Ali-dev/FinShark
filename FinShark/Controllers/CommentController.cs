using FinShark.Dtos;
using FinShark.Mappers;
using FinShark.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments = await _unitOfWork.Comment.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!_unitOfWork.Comment.ObjectExistAsync(u => u .Id == id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var comment = await _unitOfWork.Comment.GetAsync(s => s.Id == id);

            return Ok(comment.ToCommentDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto dto)
        {
            if(!_unitOfWork.Stock.ObjectExistAsync(u => u.Id == dto.StockId).GetAwaiter().GetResult())
            {
                return NotFound("Stock Not Found");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var commentModel = dto.ToCommentModel();
            await _unitOfWork.Comment.CreateAsync(commentModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

        }
        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update([FromRoute] int commentId, [FromBody] CreateCommentDto dto)
        {
            if (!_unitOfWork.Comment.ObjectExistAsync(U => U.Id == commentId).GetAwaiter().GetResult())
            {
                return NotFound("comment Not Found");
            }
            if (!_unitOfWork.Stock.ObjectExistAsync(U => U.Id == dto.StockId).GetAwaiter().GetResult())
            {
                return NotFound("Stock Not Found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
      

            var commentFromDb =await _unitOfWork.Comment.Update(commentId, dto);
           await _unitOfWork.SaveAsync();
            return Ok(commentFromDb.ToCommentDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!_unitOfWork.Comment.ObjectExistAsync(u => u.Id == id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            var commentToDelete = await _unitOfWork.Comment.GetAsync(u => u.Id == id);
            _unitOfWork.Comment.Delete(commentToDelete);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
