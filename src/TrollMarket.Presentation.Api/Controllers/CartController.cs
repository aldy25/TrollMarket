using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Presentation.Api.ModelDto.Cart;
using TrollMarket.Presentation.Api.ModelDto.ResponseApi;
using TrollMarket.Presentation.Api.Services;

namespace TrollMarket.Presentation.Api.Controllers
{
    [Route("Api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public IActionResult Insert(CartModelDto cart)
        {
            var dto = _cartService.AddCart(cart);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 201,
                SatatusText = "Succes",
                Message = "Product successfully added to cart",
                Detail = dto
            };
            return Created("", response);
        }
        [Authorize(Roles = "Buyer")]
        [HttpDelete("{buyerNumber}")]
        public IActionResult AddTransaction(string buyerNumber)
        {
            try
            {
                _cartService.AddTransaction(buyerNumber);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "Successful transaction, now your cart is empty"
                };
                return Ok(response);
            }
            catch(InvalidDataException i)
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 400,
                    SatatusText = "Failed",
                    Message = i.Message,
                    Error = "Transaction Failed"
                };
                return BadRequest(response);
            }
        }
        [Authorize(Roles = "Buyer")]
        [HttpDelete("{productId}/{buyerNumber}/{shipperNumber}")]
        public IActionResult Remove(int productId, string buyerNumber, string shipperNumber)
        {
            try
            {
                _cartService.Remove(productId, buyerNumber,shipperNumber);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "Product in the cart Succes Has deleted"
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
                    Error = "Deleted Failed"
                };
                return BadRequest(response);
            }
        }

    }
}
