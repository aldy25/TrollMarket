namespace TrollMarket.Persentation.Web.Models.Cart
{
    public class CartIndexViewModel
    {
        public List<CartTableViewModel> Carts { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public string BuyerNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
