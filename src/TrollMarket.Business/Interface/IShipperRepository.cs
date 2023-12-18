using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Interface
{
    public interface IShipperRepository
    {
        public Shipper GetShipper(string shipperNumber);
        public List<Shipper> GetShippers(int page, int pageSize);
        public List<Shipper> GetShippers();
        public int CountShippers();
        public Shipper Update(Shipper shipper);
        public Shipper Insert(Shipper shipper);
        public Shipper GetShipperByShipperName(string shipperName);
        public Shipper Delete(Shipper shipper);
    }
}
