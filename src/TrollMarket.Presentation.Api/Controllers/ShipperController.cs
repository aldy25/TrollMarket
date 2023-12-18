using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Presentation.Api.ModelDto.Product;
using TrollMarket.Presentation.Api.ModelDto.ResponseApi;
using TrollMarket.Presentation.Api.ModelDto.Shipper;
using TrollMarket.Presentation.Api.Services;

namespace TrollMarket.Presentation.Api.Controllers
{
    [Route("Api/v1/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {

        private readonly ShipperService _shipperService;

        public ShipperController(ShipperService shipperService)
        {
            _shipperService = shipperService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Insert(ShipperInsertModelDto dto)
        {
            var result = _shipperService.Insert(dto);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 201,
                SatatusText = "Created",
                Message = $"Shipment {result.ShipperName} added successfully",
                Detail = result
            };
            return Created("", response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update(ShipperModelDto dto)
        {
            var result = _shipperService.Update(dto);
            ResponseSucces response = new ResponseSucces
            {
                SatatusCode = 200,
                SatatusText = "Succes",
                Message = $"Shipment {result.ShipperName} Successfull Updated ",
                Detail = result
            };
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("CheckAvailableUpdateShipment/{shipperNumber}")]
        public IActionResult CheckAvailableUpdateShipment(string shipperNumber)
        {

            if (_shipperService.CheckAvailableUpdateShipper(shipperNumber))
            {
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "Shipment is Available for Update"
                };
                return Ok(response);
            }
            else
            {
                ResponseError response = new ResponseError
                {
                    SatatusCode = 401,
                    SatatusText = "Failed",
                    Message = "Shipment is Not Available for Update",
                    Error = "The Shipment cannot be changed because it has already been used"
                };
                return BadRequest(response);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{shipperNumber}")]
        public IActionResult Delete(string shipperNumber)
        {
            try
            {
                var dto = _shipperService.Delete(shipperNumber);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "Shipment Success Deleted",
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
                    Message = "the Shippment cannot be deleted",
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
                    Message = "the Shippment cannot be deleted",
                    Error = i.Message
                };
                return BadRequest(response);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("{shipperNumber}")]
        public IActionResult ShipperStopService(string shipperNumber)
        {
            try
            {
                var result = _shipperService.ShipperStopService(shipperNumber);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = $"Shipment a was successfully Stop Service",
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
                    Message = "the Shipment cannot be Stop Service",
                    Error = k.Message
                };
                return BadRequest(response);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{shipperNumber}")]
        public IActionResult GetShipper(string shipperNumber)
        {
            try
            {
                var result = _shipperService.GetShipper(shipperNumber);
                ResponseSucces response = new ResponseSucces
                {
                    SatatusCode = 200,
                    SatatusText = "Succes",
                    Message = "The Shipment was successfully picked up",
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
                    Message = "the Shipment not found",
                    Error = k.Message
                };
                return BadRequest(response);
            }
        }
    }
}
