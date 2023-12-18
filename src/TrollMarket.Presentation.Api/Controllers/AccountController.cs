using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto.Account;
using TrollMarket.Presentation.Api.ModelDto.ResponseApi;
using TrollMarket.Presentation.Api.Services;

namespace TrollMarket.Presentation.Api.Controllers
{
    [Route("Api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        [Authorize(Roles = "Buyer")]
        [HttpPatch("{id}")]
        public IActionResult TopUpDana(int id, [FromBody] AccountAddFundsModelDto dto)
        {
            var result = _accountService.TopUpBalance(id,dto);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 200,
                SatatusText =  "Succes",
                Message = $"top up funds amounting to {result.Amount.ToString("C")} Successful",
                Detail = result
            };
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Insert(AccountAddAdminDto account)
        {
            _accountService.AddAccountAdmin(account);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 201,
                SatatusText = "Success",
                Message = "Success Created new Account Admin"
            };

            return Ok(response);
        }
    }
}
