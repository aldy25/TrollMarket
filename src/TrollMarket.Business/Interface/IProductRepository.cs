using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Interface
{
    public interface IProductRepository
    {
        public Product Insert(Product product);
        public Product GetProductById(int id);
        public Product Update(Product product);
        public void Delete(Product product);
    }
}
