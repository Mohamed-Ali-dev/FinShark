using FinShark.Service;
using FinShark.Dtos;
using FinShark.Helpers;
using FinShark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IAuthService _authService;
        public AccountController(UserManager<AppUser> userManager, JwtOptions jwtOptions, IAuthService authService)
        {
            this._userManager = userManager;
            _jwtOptions = jwtOptions;
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid) 
                    return BadRequest(ModelState);

                var result = await _authService.RegisterAsync(registerDto);

                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }

                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _authService.LogIn(dto);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
        [HttpPost("addRole")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> AddRole(AddRoleDto addRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(addRoleDto);
            if(!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(addRoleDto);
        }
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken(string? inToken)
        {
            var token = inToken ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required");
            var result = await _authService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid");
            return Ok();
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
   
    }
}
