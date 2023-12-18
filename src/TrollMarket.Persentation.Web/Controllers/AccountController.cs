using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.NetworkInformation;
using System.Security.Claims;
using TrollMarket.Persentation.Web.Services;

namespace TrollMarket.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {

        private readonly AccountService _accountService;
        private readonly ILogger<AuthController> _logger;
        public AccountController(AccountService accountService, ILogger<AuthController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }
        [Authorize(Roles = "Seller, Buyer,")]
        [HttpGet("Profile")]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var id = User.FindFirst("Id")?.Value;
            var vm  = _accountService.GetProfile(int.Parse(id), role, page, pageSize);
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Add")]
        public IActionResult Insert()
        {
            return View("Insert");
        }
        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}
