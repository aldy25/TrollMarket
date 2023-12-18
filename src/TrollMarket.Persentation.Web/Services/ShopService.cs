using TrollMarket.Business.Interface;
using TrollMarket.Persentation.Web.Models;
using TrollMarket.Persentation.Web.Models.Shipper;
using TrollMarket.Persentation.Web.Models.Shop;

namespace TrollMarket.Persentation.Web.Services
{
    public class ShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IShipperRepository _shipperRepository;
        private readonly IAccountRepository _accountRepository;
        public ShopService(IShopRepository shopRepository, IShipperRepository shipperRepository, IAccountRepository accountRepository)
        {
            _shopRepository = shopRepository;
            _shipperRepository = shipperRepository;
            _accountRepository = accountRepository;
        }

        public ShopIndexViewModel GetAllProducts(int id, int page, int pageSize, string? productName, string? category, string? description)
        {
            List<ShopProductViewModel> result = _shopRepository.GetAllProducts(page, pageSize, productName, category, description)
                                    .Select(p => new ShopProductViewModel
                                    {
                                        Id = p.Id,
                                        ProductName = p.ProductName,
                                        Price = p.Price
                                    }).ToList();
            return new ShopIndexViewModel
            {
                Products = result,
                Pagination = new PaginationViewModel
                {
                    TotalItems = _shopRepository.CountAllProducts(productName, category, description),
                    PageNumber = page,
                    PageSize = pageSize
                },
                BuyerNumber = _accountRepository.GetBuyer(id).BuyerNumber,
                Shippers = GetShippers(),
                ProductName = productName,
                Category = category,
                Description = description
            };
        }

        public List<ShipperViewModel> GetShippers()
        {
            return _shipperRepository.GetShippers()
                .Select(s => new ShipperViewModel
                {
                    ShipperNumber = s.ShipperNumber,
                    ShipperName = s.ShipperName,
                    Price = s.Price,
                    Service = s.Service
                }).ToList();
        }
    }
}
