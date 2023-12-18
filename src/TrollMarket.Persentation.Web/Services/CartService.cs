using TrollMarket.Business.Interface;
using TrollMarket.Persentation.Web.Models.Cart;
using TrollMarket.Persentation.Web.Models;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Persentation.Web.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IAccountRepository _accountRepository;

        public CartService(ICartRepository cartRepository, IAccountRepository accountRepository)
        {
            _cartRepository = cartRepository;
            _accountRepository = accountRepository;
        }

        public CartIndexViewModel GetCarts(int idBuyer, int page, int pageSize)
        {
            Buyer buyer = _accountRepository.GetBuyer(idBuyer);
            string buyerNumber = buyer.BuyerNumber;
            var result = _cartRepository.GetCarts(buyerNumber, page, pageSize);
            decimal totalAmount = result.Sum(o => (o.Product.Price * o.Quantity) + o.ShipperNumberNavigation.Price);
            var carts =result.Select(c =>  new CartTableViewModel
                {
                    Cart = new CartViewModel
                    {
                        BuyerNumber = c.BuyerNumber,
                        ProductId = c.ProductId,
                        ShipperNumber = c.ShipperNumber,
                        Quantity = c.Quantity
                    },
                    ProductName = c.Product.ProductName,
                    Shipment = c.ShipperNumberNavigation.ShipperName,
                    SellerName = c.Product.SellerNumberNavigation.Name,
                    TotalPrice = (c.Quantity * c.Product.Price) + c.ShipperNumberNavigation.Price

                }).ToList();

            return new CartIndexViewModel
            {
                Carts = carts,
                Pagination =  new PaginationViewModel
                {
                    TotalItems = _cartRepository.CountCartByBuyerNumber(buyerNumber),
                    PageNumber = page,
                    PageSize = pageSize
                },
                BuyerNumber = buyerNumber,
                Balance = buyer.Balance,
                TotalAmount = totalAmount
            };
        }
    }
}
