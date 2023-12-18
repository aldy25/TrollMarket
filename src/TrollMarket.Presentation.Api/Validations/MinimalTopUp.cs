using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto.Auth;

namespace TrollMarket.Presentation.Api.Validations
{
    public class MinimalTopUp : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var dana = (decimal)value;
                if (dana<10000)
                {
                    return new ValidationResult("Minimum top up IDR 10,000!!!");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}
