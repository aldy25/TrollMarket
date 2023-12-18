using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Api.ModelDto.Auth;
using TrollMarket.Presentation.Api.ModelDto.ResponseApi;
using TrollMarket.Presentation.Api.Services;

namespace TrollMarket.Presentation.Api.Controllers
{
    [Route("Api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public IActionResult Register(AuthRegisterModelDto dto)
        {
           var result =  _authService.Register(dto);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 201,
                SatatusText = "Created",
                Message = "registration is successful, please click ok to go to the login page",
                Detail = result
            };
            return Created("",response);
        }
        [HttpPost("Login")]
        public IActionResult Login(AuthLoginModelDto dto)
        {
            try
            {
                var result = _authService.CreateToken(dto);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "login is successful",
                    Detail = result
                };
                return Ok(response);
            }
            catch (KeyNotFoundException k)
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 400,
                    SatatusText = "Failed",
                    Message = k.Message,
                    Error = "login failed"
                };
                return BadRequest(response);
            }
            
        }
    }
}
