using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;
using TrollMarket.DataAcces.Models.ModelBusiness;

namespace TrollMarket.Business.Interface
{
    public interface IAuthRepository
    {
        public RegisterModelBusiness Register(RegisterModelBusiness mb);
        public string GetGenerateBuyerNumber();
        public Account GetAccountByUsernameAndRole(string username, string role);
    }
}
