using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Interface
{
    public interface ICartRepository
    {
        public int CountCartByProductId(int id);
        public int CountCartByShipperNumber(string shipperNumber);

        public Cart Insert(Cart cart);
        public Cart Update(Cart cart);
        public Cart Get(int idProduct, string buyerNumber, string shipperNumber);
        public List<Cart> GetCarts(string buyerNumber, int page, int pageSize);
        public List<Cart> GetCarts(string buyerNumber);
        public int CountCartByBuyerNumber(string buyerNumber);
        public void AddTransaction(List<Cart> carts, Buyer buyer);
        public Cart Delete(Cart cart);
    }
}
