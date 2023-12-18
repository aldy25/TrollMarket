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
    public class CartRepository : ICartRepository
    {
        private readonly TrollMarketContext _dbContext;

        public CartRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int CountCartByProductId(int id)
        {
            return _dbContext.Carts.Where(c => c.ProductId == id).Count();
        }
        public int CountCartByShipperNumber(string shipperNumber)
        {
            return _dbContext.Carts.Where(c => c.ShipperNumber.Equals(shipperNumber)).Count();
        }
        public Cart Insert(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            _dbContext.SaveChanges();
            return cart;
        }
        public Cart Update(Cart cart)
        {
            _dbContext.Carts.Update(cart);
            _dbContext.SaveChanges();
            return cart;
        }
        public Cart Get(int idProduct, string buyerNumber, string shipperNumber)
        {
            return _dbContext.Carts.Where(c => c.ProductId == idProduct && c.BuyerNumber.Equals(buyerNumber) && c.ShipperNumber.Equals(shipperNumber)).FirstOrDefault();
        }
        public List<Cart> GetCarts(string buyerNumber, int page, int pageSize)
        {
            return _dbContext.Carts.Include(c =>c.BuyerNumberNavigation).Include(c => c.Product).Include(c => c.ShipperNumberNavigation).Include(c=> c.Product.SellerNumberNavigation).Where(c => c.BuyerNumber.Equals(buyerNumber)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<Cart> GetCarts(string buyerNumber)
        {
            return _dbContext.Carts.Include(c => c.BuyerNumberNavigation).Include(c => c.Product).Include(c => c.ShipperNumberNavigation).Include(c => c.Product.SellerNumberNavigation).Where(c => c.BuyerNumber.Equals(buyerNumber)).ToList();
        }
        public int CountCartByBuyerNumber(string buyerNumber)
        {
            return _dbContext.Carts.Where(c => c.BuyerNumber.Equals(buyerNumber)).Count();
        }
        public void AddTransaction(List<Cart> carts,Buyer buyer)
        {
            Seller seller;
            Order order;
            foreach (var cart in carts)
            {
                order = new Order
                {
                    Quantity = cart.Quantity,
                    OrderDate = DateTime.Now,
                    BuyerNumber = cart.BuyerNumber,
                    ShipperNumber = cart.ShipperNumber,
                    ProductId = cart.ProductId
                };
                seller = cart.Product.SellerNumberNavigation;
                seller.Balance = seller.Balance + (cart.Quantity * cart.Product.Price);
                _dbContext.Orders.Add(order);
                _dbContext.Carts.Remove(cart);
                _dbContext.Sellers.Update(seller);
            }
            _dbContext.Buyers.Update(buyer);
            _dbContext.SaveChanges();
        }


        public Cart Delete(Cart cart)
        {
            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();
            return cart;
        }

    }
}
