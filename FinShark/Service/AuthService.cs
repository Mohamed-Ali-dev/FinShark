using FinShark.Dtos;
using FinShark.Helpers;
using FinShark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinShark.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;
        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions;
        }
        public async Task<AuthModel> RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            {
                return new AuthModel { Message = "Email is already registered!" };
            }
            if (await _userManager.FindByEmailAsync(dto.UserName) is not null)
            {
                return new AuthModel { Message = "The user name you chose is already in use." };
            }
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                lastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
      
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors = $"{error.Description}";
                }
                return new AuthModel { Message = errors };
            }

            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_User);
            if (!roleResult.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors = $"{error.Description}";
                }
                return new AuthModel { Message = errors };
            }

            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens?.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            return new AuthModel
            {
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { SD.Role_User },
                UserName = dto.UserName,
                Token = await CreateJwtToken(user),
                ExpiresOn = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn
            };
        }

        public async Task<AuthModel> LogIn(LoginDto dto)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(dto.Email); 
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                authModel.Message = "Email Or Password is incorrect";
                return authModel;
            }
            var roleList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = await CreateJwtToken(user);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime);
            authModel.Roles = roleList.ToList();

            if(user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.ExpiresOn = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            } 
            return authModel;
        }

        public async Task<string>  CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)

            }.Union(userClaims).Union(roleClaims);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime),
                SigningCredentials = signingCredentials,
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience
            };
            var tokenHanlder = new JwtSecurityTokenHandler();
            var token = tokenHanlder.CreateToken(tokenDescriptor);

            return tokenHanlder.WriteToken(token);
        }
        public async Task<string> AddRoleAsync(AddRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(dto.Role))
                return "Invalid user Id or Role";
            if (await _userManager.IsInRoleAsync(user, dto.Role))
                return "User is already assign to this role";
            var result = await _userManager.AddToRoleAsync(user, dto.Role);
            return result.Succeeded ? String.Empty : "Something went wrong"; 

        }
        
        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();

            //check if there is a user has this token
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token)); 
            if (user == null)
            {
                authModel.IsAuthenticated = false;
                authModel.Message = "InvalidToken";
                return authModel;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                authModel.IsAuthenticated = false;
                authModel.Message = "Inactive Token";
                return authModel;
            }
            //revoke the selected refresh token and create a new refresh token and jwt token
            refreshToken.RevokedOn = DateTime.UtcNow;
            //create new refreshToken
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
            //generate new tokenA
            var jwtToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            authModel.IsAuthenticated = true;
            authModel.Token = jwtToken;
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return authModel;
        }
        public async Task<bool>  RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync( u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                return false;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if(!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true; 
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
