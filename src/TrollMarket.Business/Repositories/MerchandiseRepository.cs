using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Repositories
{
    public class MerchandiseRepository : IMerchandiseRepository
    {
        private readonly TrollMarketContext _dbContext;

        public MerchandiseRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAll(string sellerNumber, int page, int pageSize)
        {
            var query =_dbContext.Products.Where(p => p.SellerNumber.Equals(sellerNumber)).ToList();
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public int CountAllDate(string sellerNumber)
        {
            var query = _dbContext.Products.Where(p => p.SellerNumber.Equals(sellerNumber)).ToList();
            return query.Count();
        }
        public Product GetProductByIdAndSellerNumber(int id, string sellerNumber)
        {
            return _dbContext.Products.Where(p => p.Id == id && p.SellerNumber.Equals(sellerNumber)).FirstOrDefault();
        }
    }
}
