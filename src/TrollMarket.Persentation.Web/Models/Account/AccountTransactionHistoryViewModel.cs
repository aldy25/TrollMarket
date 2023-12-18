namespace TrollMarket.Persentation.Web.Models.Account
{
    public class AccountTransactionHistoryViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Shipment { get; set; }
        public decimal PriceTotal { get; set; }
    }
}
