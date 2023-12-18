namespace TrollMarket.Persentation.Web.Models.Cart
{
    public class CartViewModel
    {
        public int ProductId { get; set; }

        public string BuyerNumber { get; set; } = null!;

        public string ShipperNumber { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
