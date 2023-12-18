using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto;
using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Api.ModelDto.Auth;

namespace TrollMarket.Presentation.Api.Validations
{
    public class UsernameRoleUnique : ValidationAttribute
    {
        private readonly string _role;

        public UsernameRoleUnique(string role)
        {
            _role = role;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                object unknownObject = validationContext.ObjectInstance;
                var role = unknownObject.GetType().GetProperty(_role);
                var roleValue = (string)role.GetValue(unknownObject);
                var account = validationContext.ObjectInstance;
                string username = ((string)value).ToLower();                
                var dbContext = (TrollMarketContext)validationContext.GetService(typeof(TrollMarketContext));
                var nameExist = dbContext.Accounts.Where(c => c.Username.ToLower().Equals(username) && c.Role.Equals(roleValue)).FirstOrDefault();
                if (nameExist!=null)
                {
                    return new ValidationResult("username has been used, please use another username!!!");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}
