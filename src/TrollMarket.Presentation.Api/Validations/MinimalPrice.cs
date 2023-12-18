using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Api.Validations
{
    public class MinimalPrice : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var price = (decimal)value;
                if (price < 0)
                {
                    return new ValidationResult("Prices cannot be minus");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}
