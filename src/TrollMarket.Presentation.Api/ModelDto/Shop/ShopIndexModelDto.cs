namespace TrollMarket.Presentation.Api.ModelDto.Shop
{
    public class ShopIndexModelDto
    {
        public List<ShopProductModelDto> Products { get; set; }
        public bool LoadMore { get; set; }
    }
}
