namespace TrollMarket.Persentation.Web.Models.Cart
{
    public class CartTableViewModel
    {
        public CartViewModel Cart { get; set; }
        public string ProductName { get; set; }
        public string Shipment { get; set; }
        public string SellerName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
