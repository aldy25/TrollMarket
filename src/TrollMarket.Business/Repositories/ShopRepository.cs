using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly TrollMarketContext _dbContext;

        public ShopRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Product> GetAllProducts(int page, int pageSize, string? productName, string? category, string? description)
        {
            var query = _dbContext.Products.Where(
                    p => (p.ProductName.Contains(productName) || productName == null) && (p.Category.Contains(category) || category==null) && (p.Description.Contains(description) || description==null) && (p.Discontinue == false)
                );
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public int CountAllProducts(string? productName, string? category, string? description)
        {
            var query = _dbContext.Products.Where(
                   p => (p.ProductName.Contains(productName) || productName == null) && (p.Category.Contains(category) || category == null) && (p.Description.Contains(description) || description == null) && (p.Discontinue == false)
               );
            return query.Count();
        }
    }
}
