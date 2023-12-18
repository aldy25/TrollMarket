using System.Collections.Specialized;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Persentation.Web.Models.History;
using TrollMarket.Persentation.Web.Models;

namespace TrollMarket.Persentation.Web.Services
{
    public class HistoryService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAccountRepository _accountRepository;
        public HistoryService(IOrderRepository orderRepository, IAccountRepository accountRepository)
        {
            _orderRepository = orderRepository;
            _accountRepository = accountRepository;
        }

        public HistoryIndexViewModel GetAll(int page, int pageSize, string buyerNumber, string sellerNumber)
        {
            var result =  _orderRepository.GetAll(page, pageSize, buyerNumber, sellerNumber);
            List<HistoryTableViewModel> orders = result
                    .Select(o => new HistoryTableViewModel
                    {
                        Date = o.OrderDate,
                        SellerName = o.Product.SellerNumberNavigation.Name,
                        BuyerName = o.BuyerNumberNavigation.Name,
                        ProductName = o.Product.ProductName,
                        Quantity = o.Quantity,
                        Shipment = o.ShipperNumberNavigation.ShipperName,
                        TotalPrice = (o.Quantity*o.Product.Price) + o.ShipperNumberNavigation.Price
                    }).ToList();

            decimal totalTransactionCosts = result.Sum(o => ((o.Product.Price * o.Quantity) + o.ShipperNumberNavigation.Price));
            return new HistoryIndexViewModel
            {
                Orders = orders,
                Buyers = GetBuyers(),
                Sellers = GetSellers(),
                TotalTransactionCosts = totalTransactionCosts,
                BuyerNumber = buyerNumber,
                SellerNumber = sellerNumber,
                Pagination =  new PaginationViewModel
                {
                    TotalItems = _orderRepository.CountDataOrders(buyerNumber, sellerNumber),
                    PageNumber = page,
                    PageSize = pageSize
                }
            };
        }

        public List<HistoryDataBuyersViewModel> GetBuyers()
        {
            return _accountRepository.GetBuyers().Select(b => new HistoryDataBuyersViewModel
            {
                BuyerNumber = b.BuyerNumber,
                BuyerName = b.Name
            }).ToList();
        }
        public List<HistoryDataSellersViewModel> GetSellers()
        {
            return _accountRepository.GetSellers().Select(b => new HistoryDataSellersViewModel
            {
                SellerNumber = b.SellerNumber,
                SellerName = b.Name
            }).ToList();
        }
    }
}
