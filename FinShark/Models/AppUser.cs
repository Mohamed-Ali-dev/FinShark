using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FinShark.Models
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string lastName { get; set; }
        public string FullName => $"{FirstName} {lastName}";

        public List<RefreshToken>? RefreshTokens { get; set; }
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}
