using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Api.Validations;

namespace TrollMarket.Presentation.Api.ModelDto.Auth
{
    public class AuthRegisterModelDto
    {
        [Required]
        [UsernameRoleUnique("Role")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "password must be the same")]
        public string RetypePassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address {get; set; }
        [Required]
        public string Role { get; set; }
    }
}
