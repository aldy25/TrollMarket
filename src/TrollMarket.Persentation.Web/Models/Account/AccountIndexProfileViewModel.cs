namespace TrollMarket.Persentation.Web.Models.Account
{
    public class AccountIndexProfileViewModel
    {
        public AccountProfileViewModel Profile { get; set; }
        public List<AccountTransactionHistoryViewModel> TransactionHistory { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
