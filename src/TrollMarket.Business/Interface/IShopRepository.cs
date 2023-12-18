using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Interface
{
    public interface IShopRepository
    {
        public List<Product> GetAllProducts(int page, int pageSize,string? productName, string? category, string? description);
        public int CountAllProducts(string? productName, string? category, string? description);
    }
}
