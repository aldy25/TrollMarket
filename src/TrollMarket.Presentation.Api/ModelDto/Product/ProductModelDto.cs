using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Api.Validations;

namespace TrollMarket.Presentation.Api.ModelDto.Product
{
    public class ProductModelDto
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public string Category { get; set; } = null!;
        [Required]
        [MinimalPrice]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        [Required]
        public bool Discontinue { get; set; }
        [Required]
        public string SellerNumber { get; set; } = null!;
    }
}
