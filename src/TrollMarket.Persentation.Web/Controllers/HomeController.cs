using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using TrollMarket.Persentation.Web.Models;
using TrollMarket.Persentation.Web.Services;

namespace TrollMarket.Persentation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;

        public HomeController(ILogger<HomeController> logger, HomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        [Authorize(Roles = "Buyer,Seller,Admin")]
        public IActionResult Index()
        {
            bool showNotification = TempData["DataLogin"] != null && (bool)TempData["DataLogin"];

            if (showNotification)
            {
                ViewData["ShowNotification"] = true;
            }
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var id = User.FindFirst("Id")?.Value;
            if (role.Equals("Buyer"))
            {
                var vm =_homeService.GetHistoryBuyer(int.Parse(id));
                return View("DashboardBuyer", vm);
            }
            else if (role.Equals("Seller"))
            {
                var vm = _homeService.GetHistorySeller(int.Parse(id));
                return View("DashboardSeller", vm);
            }
            else
            {
                var vm = _homeService.GetHistoryAdmin();
                return View("DashboardAdmin", vm);
            }
        }

        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}