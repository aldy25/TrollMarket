using TrollMarket.Persentation.Web.Models.Shipper;

namespace TrollMarket.Persentation.Web.Models.Shop
{
    public class ShopIndexViewModel
    {
        public List<ShopProductViewModel> Products { get; set; }
        public PaginationViewModel Pagination { get; set; } 
        public List<ShipperViewModel> Shippers { get; set; }
        public string BuyerNumber { get; set; } 
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

    }
}
