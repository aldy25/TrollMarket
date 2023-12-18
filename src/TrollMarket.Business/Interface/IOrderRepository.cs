using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;
namespace TrollMarket.Business.Interface
{
    public interface IOrderRepository
    {
        public int CountOrderByProductId(int id);
        public int CountOrderByShipperNumber(string shipperNumber);
        public List<Order> GetAll(int page, int pageSize, string? buyerNumber, string? sellerNumber);
        public List<Order> GetAll();
        public int CountDataOrders(string? buyerNumber, string? sellerNumber);

        public List<Order> GetOrdersByBuyerNumber(string buyerNumber);
        public List<Order> GetOrdersBySellerNumber(string sellerNumber);
    }
}
