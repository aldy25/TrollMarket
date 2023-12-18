using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models.ModelBusiness;
using TrollMarket.Presentation.Api.ModelDto.Auth;

namespace TrollMarket.Presentation.Api.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public AuthResponseRegisterModelDto Register(AuthRegisterModelDto dto)
        {
            RegisterModelBusiness mb = new RegisterModelBusiness
            {
                UserName = dto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Name = dto.Name,
                Address = dto.Address,
                Role = dto.Role
            };
            var result =  _authRepository.Register(mb);
            return new AuthResponseRegisterModelDto
            {
                UserName = result.UserName,
                UserNumber = result.Password,
                Name = result.Name,
                Address = result.Address,
                Role = result.Role
            };
        }

        public AuthTokenModelDto CreateToken(AuthLoginModelDto dto)
        {
            var account = _authRepository.GetAccountByUsernameAndRole(dto.Username,dto.Role)
                ?? throw new KeyNotFoundException("Incorrect username or password");
            //proses verifikasi antara password di database dan password yang dimasukan database
            bool iscorectPassword = BCrypt.Net.BCrypt.Verify(dto.Password, account.Password);
            if (!iscorectPassword)
            {
                throw new KeyNotFoundException("Incorrect username or password");
            }
            //masukan data header

            var algorithm = SecurityAlgorithms.HmacSha256;

            //masukan data payload
            var payload = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Username),
                new Claim(ClaimTypes.Role, account.Role)
            };
            //masukan data signature

            var signature = _configuration.GetSection("AppSettings:TokenSignature").Value;
            var encodeSignature = Encoding.UTF8.GetBytes(signature);
            var token = new JwtSecurityToken(
                claims: payload,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(encodeSignature), algorithm)
                );

            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthTokenModelDto { Token = serializedToken };
        }
    }
}
