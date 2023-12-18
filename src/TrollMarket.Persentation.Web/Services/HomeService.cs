using TrollMarket.Business.Interface;
using TrollMarket.Persentation.Web.Models;
using TrollMarket.Persentation.Web.Models.History;
using TrollMarket.Persentation.Web.Models.Home;

namespace TrollMarket.Persentation.Web.Services
{
    public class HomeService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAccountRepository _accountRepository;

        public HomeService(IOrderRepository orderRepository, IAccountRepository accountRepository)
        {
            _orderRepository = orderRepository;
            _accountRepository = accountRepository;
        }

        public HomeBuyerViewModel GetHistoryBuyer(int id)
        {
            var buyer = _accountRepository.GetBuyer(id);
            var result = _orderRepository.GetOrdersByBuyerNumber(buyer.BuyerNumber);
            List<HomeHistoryViewModel> orders = result
                    .Select(o => new HomeHistoryViewModel
                    {
                        OrderDate = o.OrderDate,
                        TotalPrice = (o.Quantity * o.Product.Price) + o.ShipperNumberNavigation.Price
                    }).ToList();

            var resultTopUp = _accountRepository.GetTopups(buyer.BuyerNumber);
            List<HomeHistoryTopUpViewModel> topUps = resultTopUp
                    .Select(t => new HomeHistoryTopUpViewModel
                    {
                        Date = t.TopUpDate,
                        Amount = t.Amount
                    }).ToList();

            return new HomeBuyerViewModel
            {
                Histories = orders,
                TopUps = topUps
            };
        }
        public HomeSellerViewModel GetHistorySeller(int id)
        {
            var seller = _accountRepository.GetSeller(id);
            var result = _orderRepository.GetOrdersBySellerNumber(seller.SellerNumber);
            List<HomeHistoryViewModel> orders = result
                    .Select(o => new HomeHistoryViewModel
                    {
                        OrderDate = o.OrderDate,
                        TotalPrice = (o.Quantity * o.Product.Price) + o.ShipperNumberNavigation.Price
                    }).ToList();

            return new HomeSellerViewModel
            {
                Histories = orders
            };
        }
        public HomeAdminViewModel GetHistoryAdmin()
        {
            var result = _orderRepository.GetAll();
            List<HomeHistoryViewModel> orders = result
                    .Select(o => new HomeHistoryViewModel
                    {
                        OrderDate = o.OrderDate,
                        TotalPrice = (o.Quantity * o.Product.Price) + o.ShipperNumberNavigation.Price
                    }).ToList();

            return new HomeAdminViewModel
            {
                Histories = orders
            };
        }
    }
}
