using TrollMarket.Business.Interface;
using TrollMarket.Business.Repositories;
using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto.Product;
using TrollMarket.Presentation.Api.ModelDto.Shipper;

namespace TrollMarket.Presentation.Api.Services
{
    public class ShipperService
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public ShipperService(IShipperRepository shipperRepository, IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _shipperRepository = shipperRepository;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public ShipperModelDto Insert(ShipperInsertModelDto dto)
        {
            Shipper shipper = new Shipper
            {
                ShipperNumber = $"SHIP{_shipperRepository.CountShippers() + 1}",
                ShipperName = dto.ShipperName,
                Price = dto.Price,
                Service = dto.Service
            };
            var result = _shipperRepository.Insert(shipper);
            return new ShipperModelDto
            {
                ShipperNumber = result.ShipperNumber,
                ShipperName = result.ShipperName,
                Price = result.Price,
                Service = result.Service
            };
        }

        public ShipperModelDto Update(ShipperModelDto dto)
        {
            Shipper shipper = new Shipper
            {
                ShipperNumber = dto.ShipperNumber,
                ShipperName = dto.ShipperName,
                Price = dto.Price,
                Service = dto.Service
            };
            Shipper result = _shipperRepository.Update(shipper);
            return new ShipperModelDto
            {
                ShipperNumber = result.ShipperNumber,
                ShipperName = result.ShipperName,
                Price = result.Price,
                Service = result.Service
            };
        }
        public bool CheckAvailableUpdateShipper(string shipperNumber)
        {
            if (_cartRepository.CountCartByShipperNumber(shipperNumber) > 0)
            {
                return false;
            }
            else if (_orderRepository.CountOrderByShipperNumber(shipperNumber) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public ShipperModelDto Delete(string shipperNumber)
        {
            Shipper shipper = _shipperRepository.GetShipper(shipperNumber)
                ?? throw new KeyNotFoundException($"roduct with Shipper Number : {shipperNumber} not found");
            if (_cartRepository.CountCartByShipperNumber(shipperNumber) > 0)
            {
                throw new InvalidOperationException("The Shipment cannot be deleted because it has already been used");
            }
            else if (_orderRepository.CountOrderByShipperNumber(shipperNumber) > 0)
            {
                throw new InvalidOperationException("The Shipment cannot be deleted because it has already been used");
            }
            else
            {
               
                Shipper result =  _shipperRepository.Delete(shipper);
                return new ShipperModelDto
                {
                    ShipperNumber = result.ShipperNumber,
                    ShipperName =  result.ShipperName,
                    Price = result.Price,
                    Service = result.Service
                };
            }
        }
        public ShipperModelDto ShipperStopService(string shipperNumber)
        {
            Shipper shipper = _shipperRepository.GetShipper(shipperNumber)
                ?? throw new KeyNotFoundException($"roduct with Shipper Number : {shipperNumber} not found");
            shipper.Service = false;
            Shipper result = _shipperRepository.Update(shipper);
            return new ShipperModelDto
            {
                ShipperNumber = result.ShipperNumber,
                ShipperName = result.ShipperName,
                Price = result.Price,
                Service = result.Service
            };
        }
        public ShipperModelDto GetShipper(string shipperNumber)
        {
            Shipper result = _shipperRepository.GetShipper(shipperNumber)
                ?? throw new KeyNotFoundException($"roduct with Shipper Number : {shipperNumber} not found");
            return new ShipperModelDto
            {
                ShipperNumber = result.ShipperNumber,
                ShipperName = result.ShipperName,
                Price = result.Price,
                Service = result.Service
            };
        }
    }
}
