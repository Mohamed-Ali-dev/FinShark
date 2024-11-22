using FinShark.Dtos;
using FinShark.Helpers;
using FinShark.Mappers;
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Get([FromQuery] CommentQueryObject commentQueryObject)
        {
            IEnumerable<Comment> comments;

            if(commentQueryObject.Symbol != null) {
                comments = await _unitOfWork.Comment.GetAllAsync(s => s.Stock.Symbol.ToLower() == commentQueryObject.Symbol.ToLower()
        , includeProperties: new[] { "AppUser"}, orderBy: c => c.CreatedOn, isDescending: commentQueryObject.IsDecsending);
            }
            else
            {
                comments = await _unitOfWork.Comment.GetAllAsync( includeProperties: new[] { "AppUser"}, orderBy: c => c.CreatedOn, isDescending: commentQueryObject.IsDecsending);
            }
        

            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet("{id:int}")]
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
            var comment = await _unitOfWork.Comment.GetAsync(s => s.Id == id, includeProperties: new[] { "AppUser" });

            return Ok(comment.ToCommentDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto dto)
        {
            if (!_unitOfWork.Stock.ObjectExistAsync(u => u.Id == dto.StockId).GetAwaiter().GetResult())
            {
                return NotFound("Stock Not Found");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";

            var commentModel = dto.ToCommentModel();
            commentModel.AppUserId = userId;

            await _unitOfWork.Comment.CreateAsync(commentModel);
            await _unitOfWork.SaveAsync();
            var createdComment = await _unitOfWork.Comment.GetAsync(s => s.Id == commentModel.Id, includeProperties: new[] { "AppUser" });
            return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, createdComment.ToCommentDto());

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
            var updatedComment = await _unitOfWork.Comment.GetAsync(s => s.Id == commentFromDb.Id, includeProperties: new[] { "AppUser" });
            await _unitOfWork.SaveAsync();

            return Ok(updatedComment.ToCommentDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!_unitOfWork.Comment.ObjectExistAsync(u => u.Id == id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var commentToDelete = await _unitOfWork.Comment.GetAsync(u => u.Id == id);
            _unitOfWork.Comment.Delete(commentToDelete);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
