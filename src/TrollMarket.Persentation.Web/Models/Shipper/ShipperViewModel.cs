namespace TrollMarket.Persentation.Web.Models.Shipper
{
    public class ShipperViewModel
    {
        public string ShipperNumber { get; set; } = null!;

        public string? ShipperName { get; set; }

        public decimal Price { get; set; }

        public bool Service { get; set; }
    }
}
