using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto.Cart;

namespace TrollMarket.Presentation.Api.Services
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

        public CartModelDto AddCart(CartModelDto dto)
        {
            var checkCart = _cartRepository.Get(dto.ProductId, dto.BuyerNumber,dto.ShipperNumber);
            if (checkCart==null)
            {
                Cart cart = new Cart
                {
                    ProductId = dto.ProductId,
                    BuyerNumber = dto.BuyerNumber,
                    ShipperNumber = dto.ShipperNumber,
                    Quantity = dto.Quantity,
                };
                Cart result = _cartRepository.Insert(cart);
                return new CartModelDto
                {
                    ProductId = result.ProductId,
                    BuyerNumber = result.BuyerNumber,
                    ShipperNumber = result.ShipperNumber,
                    Quantity = result.Quantity
                };
            }
            else
            {
                checkCart.Quantity= checkCart.Quantity + dto.Quantity;
                Cart result = _cartRepository.Update(checkCart);
                return new CartModelDto
                {
                    ProductId = result.ProductId,
                    BuyerNumber = result.BuyerNumber,
                    ShipperNumber = result.ShipperNumber,
                    Quantity = result.Quantity
                };
            }
        }
        public void AddTransaction(string buyerNumber)
        {
            var carts = _cartRepository.GetCarts(buyerNumber);
            if (carts.Count > 0)
            {
                Buyer buyer = _accountRepository.GetBuyer(buyerNumber);
                decimal totalPrice = carts.Sum(cart => ((cart.Product.Price * cart.Quantity) + cart.ShipperNumberNavigation.Price));
                if (totalPrice > buyer.Balance)
                {
                    throw new InvalidDataException("Sorry, your balance is low, please increase your balance to be able to buy products");
                }
                else
                {
                    buyer.Balance = buyer.Balance - totalPrice;
                    _cartRepository.AddTransaction(carts,buyer);
                }
            }
            else
            {
                throw new InvalidDataException("Sorry, there is no data in the shopping cart, please add the product to the cart first");
            }
           
        }
        public CartModelDto Remove(int productId, string buyerNumber, string shipperNumber)
        {
            Cart cart = _cartRepository.Get(productId, buyerNumber, shipperNumber)
                ?? throw new KeyNotFoundException("Cart Not Found");
            _cartRepository.Delete(cart);
            return new CartModelDto
            {
                ProductId = cart.ProductId,
                BuyerNumber = cart.BuyerNumber,
                ShipperNumber = cart.ShipperNumber,
                Quantity = cart.Quantity
            };
        }
    }
}
