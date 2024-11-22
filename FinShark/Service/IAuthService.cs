using FinShark.Dtos;
using FinShark.Models;

namespace FinShark.Service
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto dto);
        Task<AuthModel> LogIn(LoginDto dto);
        Task<string> AddRoleAsync(AddRoleDto dto);

        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);

    }
}
