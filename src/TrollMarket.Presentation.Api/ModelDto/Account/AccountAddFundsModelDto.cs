using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Api.Validations;
namespace TrollMarket.Presentation.Api.ModelDto.Account
{
    public class AccountAddFundsModelDto
    {
        [Required]
        public string PaymentMethod { get; set; }
        [MinimalTopUp]
        [Required]
        public decimal Amount { get; set; }
    }
}
