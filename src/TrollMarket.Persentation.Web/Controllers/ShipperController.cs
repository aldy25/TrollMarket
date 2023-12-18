using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Persentation.Web.Services;

namespace TrollMarket.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class ShipperController : Controller
    {
        private readonly ShipperService _shipperService;

        public ShipperController(ShipperService shippingService)
        {
            _shipperService = shippingService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index(int page=1, int pageSize= 10)
        {
            var vm = _shipperService.GetAll(page, pageSize);
            return View(vm);
        }

        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}
