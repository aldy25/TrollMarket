using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Persentation.Web.Models.Account;
using TrollMarket.Persentation.Web.Models;
using System;

namespace TrollMarket.Persentation.Web.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountIndexProfileViewModel GetProfile(int accountId, string role, int page, int pageSize)
        {
            if (role.Equals("Buyer"))
            {
              
                Buyer buyer = _accountRepository.GetBuyer(accountId);
                var result = _accountRepository.OrderHistoryByBuyerNumber(accountId,page,pageSize);
                decimal totalPrice = result.Sum(o => ((o.Product.Price * o.Quantity) + o.ShipperNumberNavigation.Price));
                List<AccountTransactionHistoryViewModel> history = result
                                        .Select (o => new AccountTransactionHistoryViewModel
                                        {
                                            TransactionDate = o.OrderDate,
                                            ProductName = o.Product.ProductName,
                                            Quantity = o.Quantity,
                                            Shipment = o.ShipperNumberNavigation.ShipperName,
                                            PriceTotal = (o.Quantity * o.Product.Price) + o.ShipperNumberNavigation.Price
                                        })
                                        .ToList ();

                return new AccountIndexProfileViewModel
                {
                    Profile = new AccountProfileViewModel
                    {
                        Name = buyer.Name,
                        Role = role,
                        Address = buyer.Address,
                        Balannce = buyer.Balance
                    },
                    TransactionHistory = history,
                    Pagination = new PaginationViewModel
                    {
                        TotalItems = 0,
                        PageNumber = page,
                        PageSize = pageSize
                    },
                    TotalPrice = totalPrice
                };
            }else if (role.Equals("Seller"))
            {
                Seller seller =  _accountRepository.GetSeller(accountId);
                var result = _accountRepository.OrderHistoryBySellerNumber(accountId, page, pageSize);
                decimal totalPrice = result.Sum(o => ((o.Product.Price * o.Quantity) + o.ShipperNumberNavigation.Price));
                List<AccountTransactionHistoryViewModel> history = result
                                        .Select(o => new AccountTransactionHistoryViewModel
                                        {
                                            TransactionDate = o.OrderDate,
                                            ProductName = o.Product.ProductName,
                                            Quantity = o.Quantity,
                                            Shipment = o.ShipperNumberNavigation.ShipperName,
                                            PriceTotal = (o.Quantity * o.Product.Price) + o.ShipperNumberNavigation.Price
                                        })
                                        .ToList();
                return new AccountIndexProfileViewModel
                {
                    Profile = new AccountProfileViewModel
                    {
                        Name = seller.Name,
                        Role = role,
                        Address = seller.Address,
                        Balannce = seller.Balance
                    },
                    TransactionHistory = history,
                    Pagination = new PaginationViewModel
                    {
                        TotalItems = 0,
                        PageNumber = page,
                        PageSize = pageSize
                    },
                    TotalPrice = totalPrice
                };
            }
            else
            {
                throw new KeyNotFoundException("key not found");
            }
        }
    }
}
