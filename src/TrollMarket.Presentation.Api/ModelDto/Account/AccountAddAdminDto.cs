using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Api.Validations;

namespace TrollMarket.Presentation.Api.ModelDto.Account
{
    public class AccountAddAdminDto
    {
        [Required]
        [UsernameRoleUnique("Role")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "password must be the same")]
        public string RetypePassword { get; set; }

        public string Role { get; } = "Admin";
    }
}
