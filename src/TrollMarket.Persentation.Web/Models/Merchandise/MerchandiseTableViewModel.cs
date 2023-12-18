namespace TrollMarket.Persentation.Web.Models.Merchandise
{
    public class MerchandiseTableViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public string Category { get; set; } = null!;

        public bool Discontinue { get; set; }

    }
}
