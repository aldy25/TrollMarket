using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Persentation.Web.Services;

namespace TrollMarket.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly ILogger<MerchandiseController> _logger;

        private readonly ShopService _shopService;

        public ShopController(ILogger<MerchandiseController> logger, ShopService shopService)
        {
            _logger = logger;
            _shopService = shopService;
        }
        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public IActionResult Index(string? productName, string? category, string? description,int page = 1, int pageSize = 12)
        {
            var id = User.FindFirst("Id")?.Value;
            var vm = _shopService.GetAllProducts(int.Parse(id),page, pageSize, productName, category, description);
            return View(vm);
        }

        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}
