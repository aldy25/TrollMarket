using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Persentation.Web.Models.Auth;

namespace TrollMarket.Persentation.Web.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public AuthenticationTicket Login(LoginViewModel vm)
        {
            var account = _authRepository.GetAccountByUsernameAndRole(vm.Username,vm.Role)
               ?? throw new Exception("Incorrect username or password");
            //proses verifikasi antara password di database dan password yang dimasukan database
            bool iscorectPassword = BCrypt.Net.BCrypt.Verify(vm.Password, account.Password);
            if (!iscorectPassword)
            {
                throw new Exception("Incorrect username or password");
            }
            ClaimsPrincipal principal = GetPrincipal(account);
            return GetAuthenticationTicket(principal);
        }
        private ClaimsPrincipal GetPrincipal(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim("username",account.Username),
                new Claim("id",account.Id.ToString()),
                new Claim(ClaimTypes.Role, account.Role)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
        private AuthenticationTicket GetAuthenticationTicket(ClaimsPrincipal principal)
        {
            AuthenticationProperties authenticationProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(60),
                AllowRefresh = false,
            };
            AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties,
                                                                                            CookieAuthenticationDefaults.AuthenticationScheme);
            return authenticationTicket;
        }
    }
}
