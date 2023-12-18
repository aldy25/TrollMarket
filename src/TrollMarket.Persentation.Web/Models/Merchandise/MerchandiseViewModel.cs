namespace TrollMarket.Persentation.Web.Models.Merchandise
{
    public class MerchandiseViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public string Category { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public bool Discontinue { get; set; }
        public string SellerNumber { get; set; }

    }
}
