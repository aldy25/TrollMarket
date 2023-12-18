using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Presentation.Api.ModelDto.Account;
using TrollMarket.Presentation.Api.ModelDto.Product;
using TrollMarket.Presentation.Api.ModelDto.ResponseApi;
using TrollMarket.Presentation.Api.Services;

namespace TrollMarket.Presentation.Api.Controllers
{
    [Route("Api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Seller")]
        [HttpPost]
        public IActionResult Insert(ProductModelDto dto)
        {
            var result = _productService.Insert(dto);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 201,
                SatatusText = "Created",
                Message = $"Product {result.ProductName} added successfully",
                Detail = result
            };
            return Created("", response);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut]
        public IActionResult Update(ProductModelDto dto)
        {
            var result = _productService.Update(dto);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 200,
                SatatusText = "Succes",
                Message = $"Product {result.ProductName} Successfull Updated ",
                Detail = result
            };
            return Ok(response);
        }
        [Authorize(Roles = "Seller")]
        [HttpGet("CheckAvailableUpdateProduct/{id}")]
        public IActionResult GetProductById(int id)
        {
           
            if (_productService.CheckAvailableUpdateProduct(id))
            {
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "Product is Available for Update"
                };
                return Ok(response);
            }
            else
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 401,
                    SatatusText = "Failed",
                    Message = "Product is Not Available for Update",
                    Error = "The product cannot be changed because it has already been used"
                };
                return BadRequest(response);
            }
        }
        [Authorize(Roles = "Seller")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dto = _productService.Delete(id);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "Product is Available for Update",
                    Detail = dto
                };
                return Ok(response);
            }
            catch (KeyNotFoundException k)
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 404,
                    SatatusText = "Failed",
                    Message = "the product cannot be deleted",
                    Error = k.Message
                };
                return BadRequest(response);
            }
            catch (InvalidOperationException i)
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 401,
                    SatatusText = "Failed",
                    Message = "the product cannot be deleted",
                    Error = i.Message
                };
                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Seller")]
        [HttpPatch("{id}")]
        public IActionResult PatchProduct(int id)
        {
            try
            {
                var result = _productService.ProductDiscontinue(id);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = $"product a was successfully discontinued",
                    Detail = result
                };
                return Ok(response);
            }
            catch (KeyNotFoundException k)
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 404,
                    SatatusText = "Failed",
                    Message = "the product cannot be discontinued",
                    Error = k.Message
                };
                return BadRequest(response);
            }
            
        }
        [Authorize(Roles = "Seller")]
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var result = _productService.GetProductById(id);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "The product was successfully picked up",
                    Detail = result
                };
                return Ok(response);
            }
            catch (KeyNotFoundException k)
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 404,
                    SatatusText = "Failed",
                    Message = "the product not found",
                    Error = k.Message
                };
                return BadRequest(response);
            }
        }
    }
}
