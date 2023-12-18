using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Persentation.Web.Models.Merchandise;
using TrollMarket.Persentation.Web.Models;

namespace TrollMarket.Persentation.Web.Services
{
    public class MerchandiseService
    {
        private readonly IMerchandiseRepository _merchandiseRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        public MerchandiseService(IMerchandiseRepository merchandiseRepository, IAccountRepository accountRepository, ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            _merchandiseRepository = merchandiseRepository;
            _accountRepository = accountRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public MerchandiseIndexViewModel GetAll(int id, int page,int pageSize)
        {
            string sellerNumber = _accountRepository.GetSeller(id).SellerNumber;
            List<MerchandiseTableViewModel> products = _merchandiseRepository.GetAll(sellerNumber, page, pageSize)
                                        .Select(p => new MerchandiseTableViewModel
                                        {
                                            Id = p.Id,
                                            ProductName = p.ProductName,
                                            Category = p.Category,
                                            Discontinue = p.Discontinue
                                        }).ToList();
            return new MerchandiseIndexViewModel
            {
                Merchandises = products,
                Pagination =  new PaginationViewModel
                {
                    TotalItems = _merchandiseRepository.CountAllDate(sellerNumber),
                    PageNumber = page,
                    PageSize = pageSize
                }
            };
        }
        public string GetSellerNumberByAccountId(int accountId)
        {
            return _accountRepository.GetSeller(accountId).SellerNumber;
        }
        public MerchandiseViewModel GetMerchandiseByIdAndSellerNumber(int id, string sellerNumber)
        {
            var result = _merchandiseRepository.GetProductByIdAndSellerNumber(id, sellerNumber)
                ?? throw new KeyNotFoundException($"Can't access this page");
            if (_cartRepository.CountCartByProductId(id) > 0)
            {
                throw new InvalidOperationException("The product cannot be changed because it has already been used");
            }else if (_orderRepository.CountOrderByProductId(id)>0)
            {
                throw new InvalidOperationException("The product cannot be changed because it has already been used");
            }
            else
            {
                return new MerchandiseViewModel
                {
                    Id = result.Id,
                    ProductName = result.ProductName,
                    Category = result.Category,
                    Price = result.Price,
                    Description = result.Description,
                    Discontinue = result.Discontinue,
                    SellerNumber = result.SellerNumber,
                };
            }
        }
    }
}
