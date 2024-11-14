using FinShark.Helpers;
using FinShark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        public AccountController(UserManager<AppUser> userManager, JwtOptions jwtOptions)
        {
            this._userManager = userManager;
            _jwtOptions = jwtOptions;
        }
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] registerDto)
        //{

        //}
    }
}
