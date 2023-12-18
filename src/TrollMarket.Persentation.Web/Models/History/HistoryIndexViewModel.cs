using TrollMarket.DataAcces.Models;

namespace TrollMarket.Persentation.Web.Models.History
{
    public class HistoryIndexViewModel
    {
        public List<HistoryTableViewModel> Orders { get; set; }
        public List<HistoryDataBuyersViewModel> Buyers { get; set; }
        public List<HistoryDataSellersViewModel> Sellers { get; set; }
        public decimal TotalTransactionCosts { get; set; }
        public string? SellerNumber { get; set; }
        public string? BuyerNumber { get; set; }
        public PaginationViewModel Pagination { get; set; }

    }
}
