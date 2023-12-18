using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TrollMarketContext _dbContext;

        public ProductRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product Insert(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return _dbContext.Products.OrderByDescending(p => p.Id).FirstOrDefault();
        }
        public Product GetProductById(int id)
        {
            return _dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
        }
        public Product Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return GetProductById(product.Id);
        }
        public void Delete(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
