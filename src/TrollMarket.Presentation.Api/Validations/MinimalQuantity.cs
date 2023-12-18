using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Api.Validations
{
    public class MinimalQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var quantity = (int)value;
                if (quantity <= 0)
                {
                    return new ValidationResult("quantity does not meet");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}
