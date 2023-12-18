using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.Business.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly TrollMarketContext _db_contect;

        public ShipperRepository(TrollMarketContext db_contect)
        {
            _db_contect = db_contect;
        }
        public Shipper GetShipper(string shipperNumber)
        {
            return _db_contect.Shippers.Where(s => s.ShipperNumber.Equals(shipperNumber)).FirstOrDefault();
        }

        public List<Shipper> GetShippers(int page, int pageSize) 
        {
            return _db_contect.Shippers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<Shipper> GetShippers()
        {
            return _db_contect.Shippers.Where(s => s.Service == true).ToList();
        }
        public int CountShippers()
        {
            return _db_contect.Shippers.Count();
        }
        public Shipper Update(Shipper shipper)
        {
            _db_contect.Shippers.Update(shipper);
            _db_contect.SaveChanges();
            return shipper;
        }
        public Shipper Insert(Shipper shipper)
        {
            _db_contect.Shippers.Add(shipper);
            _db_contect.SaveChanges();
            return shipper;
        }
        public Shipper GetShipperByShipperName(string shipperName)
        {
            return _db_contect.Shippers.Where(s => s.ShipperName.Equals(shipperName)).FirstOrDefault();
        }
        public Shipper Delete(Shipper shipper)
        {
            _db_contect.Remove(shipper);
            _db_contect.SaveChanges();
            return shipper;
        }
    }
}
