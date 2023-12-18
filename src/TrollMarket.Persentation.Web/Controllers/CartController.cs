using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Persentation.Web.Services;

namespace TrollMarket.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ILogger<AuthController> _logger;
        public CartController(CartService cartService, ILogger<AuthController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [Authorize(Roles = "Buyer,")]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var id = User.FindFirst("Id")?.Value;
            var vm = _cartService.GetCarts(int.Parse(id), page, pageSize);
            return View(vm);
        }
        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}
