using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Presentation.Api.ModelDto.ResponseApi;
using TrollMarket.Presentation.Api.Services;

namespace TrollMarket.Presentation.Api.Controllers
{
    [Route("Api/v1/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }
        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public IActionResult GetAllProduct(string? productName, string? category, string? description, int page = 1, int pageSize = 12)
        {
            var dto = _shopService.GetAllProducts(page,pageSize,productName, category,description);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 200,
                SatatusText= "Succes",
                Message = "Successful Retrieval of Product Data",
                Detail = dto
            };
            return Ok(response);
        }
    }
}
