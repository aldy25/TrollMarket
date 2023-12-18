namespace TrollMarket.Persentation.Web.Models.History
{
    public class HistoryTableViewModel
    {
        public DateTime Date { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Shipment { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
