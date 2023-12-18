using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Api.Validations;

namespace TrollMarket.Presentation.Api.ModelDto.Shipper
{
    public class ShipperModelDto
    {
        [Required]
        public string ShipperNumber { get; set; } = null!;
        [Required]
        public string? ShipperName { get; set; }
        [Required]
        [MinimalPrice]
        public decimal Price { get; set; }
        [Required]
        public bool Service { get; set; }
    }
}
