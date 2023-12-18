using TrollMarket.Business.Interface;
using TrollMarket.Business.Repositories;
using TrollMarket.Persentation.Web.Models.Shipper;
using TrollMarket.Persentation.Web.Models;


namespace TrollMarket.Persentation.Web.Services
{
    public class ShipperService
    {
        private readonly IShipperRepository _shipperRepository;
        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }
        public ShipperIndexViewModel GetAll(int page, int pageSize)
        {
            List<ShipperViewModel> result =  _shipperRepository.GetShippers(page, pageSize)
                                .Select(s => new ShipperViewModel
                                {
                                    ShipperNumber = s.ShipperNumber,
                                    ShipperName = s.ShipperName,
                                    Price = s.Price,
                                    Service = s.Service
                                }).ToList();
            return new ShipperIndexViewModel
            {
                Shippers = result,
                Pagination = new PaginationViewModel
                {
                    TotalItems = _shipperRepository.CountShippers(),
                    PageNumber = page,
                    PageSize = pageSize
                }
            };
        }
    }
}
