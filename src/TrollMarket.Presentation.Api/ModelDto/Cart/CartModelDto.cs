using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Api.Validations;

namespace TrollMarket.Presentation.Api.ModelDto.Cart
{
    public class CartModelDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string BuyerNumber { get; set; } = null!;
        [Required]
        public string ShipperNumber { get; set; } = null!;
        [Required]
        [MinimalQuantity]
        public int Quantity { get; set; }
    }
}
