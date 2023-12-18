using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Interface
{
    public interface IAccountRepository
    {
        public Buyer GetBuyer(int AccountId);
        public Buyer GetBuyer(string buyyerNumber);
        public Seller GetSeller(int accointId);
        public List<Buyer> GetBuyers();
        public List<Seller> GetSellers();
        public HistoryToUp TopUpDana(Buyer buyer, HistoryToUp historyToUp);

        public List<HistoryToUp> GetTopups(string buyerNumber);
        public List<Order> OrderHistoryByBuyerNumber(int accountId, int page, int pageSize);
        public List<Order> OrderHistoryBySellerNumber(int accountId, int page, int pageSize);
        public int CountDataOrderHistoryByBuyerNumber(int accountId);
        public int CountDataOrderHistoryBySellerNumber(int accountId);
        public void Insert(Account account);
    }
}
