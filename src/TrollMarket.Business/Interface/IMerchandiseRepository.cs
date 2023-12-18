using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Interface
{
    public interface IMerchandiseRepository
    {
        public List<Product> GetAll(string sellerNumber, int page, int pageSize);
        public int CountAllDate(string sellerNumber);
        public Product GetProductByIdAndSellerNumber(int id, string sellerNumber);
    }
}
