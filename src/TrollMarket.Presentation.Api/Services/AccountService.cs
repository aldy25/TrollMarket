using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto.Account;

namespace TrollMarket.Presentation.Api.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountAddFundsModelDto TopUpBalance(int AccountId, AccountAddFundsModelDto dto)
        {
            Buyer buyer = _accountRepository.GetBuyer(AccountId);
            buyer.Balance = buyer.Balance + dto.Amount;
            HistoryToUp historyToUp = new HistoryToUp
            {
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                TopUpDate = DateTime.Now,
                BuyerNumber = buyer.BuyerNumber
            };

            var result = _accountRepository.TopUpDana(buyer, historyToUp);

            return new AccountAddFundsModelDto
            {
                PaymentMethod = result.PaymentMethod,
                Amount = result.Amount,
            };
        } 

        public void AddAccountAdmin(AccountAddAdminDto dto)
        {
            Account account = new Account
            {
                Username = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };
            _accountRepository.Insert(account);
        }
    }
}
