using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.DataAcces.Models.ModelBusiness;

namespace TrollMarket.Business.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TrollMarketContext _dbContext;

        public AuthRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RegisterModelBusiness Register(RegisterModelBusiness mb)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                Account account = new Account
                {
                    Username = mb.UserName,
                    Password = mb.Password,
                    Role = mb.Role
                };
                _dbContext.Accounts.Add(account);
                _dbContext.SaveChanges();
                int id = _dbContext.Accounts.OrderByDescending(a => a.Id).FirstOrDefault().Id;

                if (mb.Role.Equals("Seller"))
                {
                    Seller seller = new Seller
                    {
                        SellerNumber = GetGenerateSellerNumber(),
                        Name = mb.Name,
                        Address = mb.Address,
                        Balance = 0,
                        AccountId = id
                    };
                    _dbContext.Sellers.Add(seller);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return new RegisterModelBusiness
                    {
                        UserNumber = seller.SellerNumber,
                        UserName = account.Username,
                        Password = account.Password,
                        Name = seller.Name,
                        Address = seller.Address,
                        Role = account.Role
                    };
                }
                else
                {
                    Buyer buyer = new Buyer
                    {
                        BuyerNumber = GetGenerateBuyerNumber(),
                        Name = mb.Name,
                        Address = mb.Address,
                        Balance = 0,
                        AccountId = id
                    };
                    _dbContext.Buyers.Add(buyer);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return new RegisterModelBusiness
                    {
                        UserNumber = buyer.BuyerNumber,
                        UserName = account.Username,
                        Password = account.Password,
                        Name = buyer.Name,
                        Address = buyer.Address,
                        Role = account.Role
                    };
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Rollback jika ada kesalahan
                throw new Exception("Sorry, the server is having problems, please try again later");
            }
        }
        public string GetGenerateBuyerNumber()
        {
            var query = _dbContext.Buyers.Count() + 1;
            return $"BUY{query}";
        }
        public string GetGenerateSellerNumber()
        {
            var query = _dbContext.Sellers.Count() + 1;
            return $"SEL{query}";
        }
        public Account GetAccountByUsernameAndRole(string username, string role)
        {
            var query = _dbContext.Accounts.Where(a => a.Username == username && a.Role == role);
            return query.FirstOrDefault();
        }
    }
}
