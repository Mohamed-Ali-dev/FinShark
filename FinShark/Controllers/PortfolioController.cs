
using FinShark.Models;
using FinShark.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public PortfolioController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            //var userName = User.GetUserName();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            
            var appUser = await _userManager.FindByIdAsync(userId);
            var userPprtfolio = await _unitOfWork.Portfolio.GetUserPortfolio(appUser);
       
            return Ok(userPprtfolio);
        }
        [HttpPost]

        public async Task<IActionResult> AddPortfolio([FromBody] string symbol)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";

            var appUser = await _userManager.FindByIdAsync(userId);

            var stock = await _unitOfWork.Stock.GetAsync(u =>u.Symbol == symbol);

            if (stock == null)
                return NotFound("Stock not found");

            var userPortfolio = await _unitOfWork.Portfolio.GetUserPortfolio(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Stock is already exist");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };
            await _unitOfWork.Portfolio.CreateAsync(portfolioModel);
            await _unitOfWork.SaveAsync();
            return Ok("Created successfully");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";

            var appUser = await _userManager.FindByIdAsync(userId);

            var userPortfolio = await _unitOfWork.Portfolio.GetAsync(e => e.Stock.Symbol.ToLower() == symbol.ToLower() && e.AppUserId == userId);
            if (userPortfolio == null)
                return NotFound("Stock Not found");
             _unitOfWork.Portfolio.Delete(userPortfolio);
            await _unitOfWork.SaveAsync();
            return NoContent();



        }
    }
}
 