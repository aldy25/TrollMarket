using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto.Shop;

namespace TrollMarket.Presentation.Api.Services
{
    public class ShopService
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public ShopIndexModelDto GetAllProducts(int page, int pageSize, string? productName, string? category, string? description)
        {
            List<Product> products = _shopRepository.GetAllProducts(page, pageSize, productName, category, description);
            List<ShopProductModelDto> result = products
                                    .Select(p => new ShopProductModelDto
                                    {
                                        Id = p.Id,
                                        ProductName = p.ProductName,
                                        Price = p.Price
                                    }).ToList();
            bool loadMore = products.Count >= 12 ? true : false;
            return new ShopIndexModelDto
            {
                Products = result,
                LoadMore = loadMore,
            };
        }
    }
}
