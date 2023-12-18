using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TrollMarketContext _dbContext;

        public OrderRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CountOrderByProductId(int id)
        {
            return _dbContext.Orders.Where(o => o.ProductId == id).Count();
        }
        public int CountOrderByShipperNumber(string shipperNumber)
        {
            return _dbContext.Orders.Where(o => o.ShipperNumber.Equals(shipperNumber)).Count();
        }
        public List<Order> GetAll(int page, int pageSize, string? buyerNumber, string? sellerNumber)
        {
            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.Product)
                .Include(o => o.Product.SellerNumberNavigation).Include(o => o.ShipperNumberNavigation)
                .Where(o => (o.BuyerNumber.Equals(buyerNumber) || buyerNumber == null) && 
                (o.Product.SellerNumber.Equals(sellerNumber) || sellerNumber == null));
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<Order> GetAll()
        {
            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.Product)
               .Include(o => o.Product.SellerNumberNavigation).Include(o => o.ShipperNumberNavigation);
            return query.ToList();
        }
        public int CountDataOrders(string? buyerNumber, string? sellerNumber)
        {
            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.Product)
                .Include(o => o.Product.SellerNumberNavigation).Include(o => o.ShipperNumberNavigation)
                .Where(o => (o.BuyerNumber.Equals(buyerNumber) || buyerNumber == null) && 
                (o.Product.SellerNumber.Equals(sellerNumber) || sellerNumber == null));
            return query.Count();
        }
        public List<Order> GetOrdersByBuyerNumber(string buyerNumber)
        {
            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.Product)
                .Include(o => o.Product.SellerNumberNavigation).Include(o => o.ShipperNumberNavigation)
                .Where(c => c.BuyerNumber.Equals(buyerNumber));
            return query.ToList();
        }
        
        public List<Order> GetOrdersBySellerNumber(string sellerNumber)
        {
            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.Product)
                .Include(o => o.Product.SellerNumberNavigation).Include(o => o.ShipperNumberNavigation)
                .Where(c => c.Product.SellerNumberNavigation.SellerNumber.Equals(sellerNumber));
            return query.ToList();
        }
    }
}
