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
    public class AccountRepository : IAccountRepository
    {
        private readonly TrollMarketContext _dbContext;

        public AccountRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Buyer GetBuyer(int AccountId)
        {
            return _dbContext.Buyers.Where(b => b.AccountId == AccountId).FirstOrDefault();
        }
        public Buyer GetBuyer(string buyyerNumber)
        {
            return _dbContext.Buyers.Where(b => b.BuyerNumber.Equals(buyyerNumber)).FirstOrDefault();
        }
        public Seller GetSeller(int accointId)
        {
            return _dbContext.Sellers.Where(s => s.AccountId == accointId).FirstOrDefault();
        }
        public List<Buyer> GetBuyers()
        {
            return _dbContext.Buyers.ToList();
        }
        public List<Seller> GetSellers()
        {
            return _dbContext.Sellers.ToList();
        }
        public HistoryToUp TopUpDana(Buyer buyer, HistoryToUp historyToUp)
        {
            _dbContext.Buyers.Update(buyer);
            _dbContext.HistoryToUps.Add(historyToUp);
            _dbContext.SaveChanges();
            return _dbContext.HistoryToUps.Where(h => h.BuyerNumber == buyer.BuyerNumber).OrderByDescending(h => h.Id).FirstOrDefault();
        }
        public List<Order> OrderHistoryByBuyerNumber(int accountId, int page, int pageSize)
        {
            Buyer buyer = _dbContext.Buyers.Where(b => b.AccountId.Equals(accountId)).FirstOrDefault();

            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.ShipperNumberNavigation).Include(o => o.Product)
                            .Where(b => b.BuyerNumber.Equals(buyer.BuyerNumber));
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<Order> OrderHistoryBySellerNumber(int accountId, int page, int pageSize)
        {
            Seller seller = _dbContext.Sellers.Where(s => s.AccountId.Equals(accountId)).FirstOrDefault();
            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.ShipperNumberNavigation).Include(o => o.Product)
                           .Where(s => s.Product.SellerNumber.Equals(seller.SellerNumber));
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public int CountDataOrderHistoryByBuyerNumber(int accountId)
        {
            Buyer buyer = _dbContext.Buyers.Where(b => b.AccountId.Equals(accountId)).FirstOrDefault();

            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.ShipperNumberNavigation).Include(o => o.Product)
                           .Where(b => b.BuyerNumber.Equals(buyer.BuyerNumber));
            return query.Count();
        }
        public int CountDataOrderHistoryBySellerNumber(int accountId)
        {
            Seller seller = _dbContext.Sellers.Where(s => s.AccountId.Equals(accountId)).FirstOrDefault();
            var query = _dbContext.Orders.Include(o => o.BuyerNumberNavigation).Include(o => o.ShipperNumberNavigation).Include(o => o.Product)
                           .Where(s => s.Product.SellerNumber.Equals(seller.SellerNumber));
            return query.Count();
        }
        public void Insert(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }
        public List<HistoryToUp> GetTopups(string buyerNumber)
        {
            return _dbContext.HistoryToUps.Where(h => h.BuyerNumber.Equals(buyerNumber)).ToList();
        }
    }
}
