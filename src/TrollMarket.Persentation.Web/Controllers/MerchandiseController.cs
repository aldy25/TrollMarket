using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using TrollMarket.Persentation.Web.Models.Merchandise;
using TrollMarket.Persentation.Web.Services;

namespace TrollMarket.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class MerchandiseController : Controller
    {
        
        private readonly  MerchandiseService _merchandiseService;
        private readonly ILogger<MerchandiseController> _logger;

        public MerchandiseController(MerchandiseService merchandiseService, ILogger<MerchandiseController> logger)
        {
            _merchandiseService = merchandiseService;
            _logger = logger;
        }
        [Authorize(Roles = "Seller")]
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var id = User.FindFirst("Id")?.Value;
            var vm = _merchandiseService.GetAll(int.Parse(id), page, pageSize);
            return View(vm);
        }
        [Authorize(Roles = "Seller")]
        [HttpGet("Add")]
        public IActionResult Add()
        {
            var id = User.FindFirst("Id")?.Value;
            var sellerNumber = _merchandiseService.GetSellerNumberByAccountId(int.Parse(id));
            MerchandiseInsertViewModel vm = new MerchandiseInsertViewModel
            {
                SellerNumber = sellerNumber
            };
            return View("Insert", vm);
        }
        [Authorize(Roles = "Seller")]
        [HttpGet("Update/{id}")]
        public IActionResult Update(int id)
        {
            try
            {
                var idAccount = User.FindFirst("Id")?.Value;
                var sellerNumber = _merchandiseService.GetSellerNumberByAccountId(int.Parse(idAccount));
                var vm = _merchandiseService.GetMerchandiseByIdAndSellerNumber(id, sellerNumber);
                return View("Update",vm);
            }
            catch (KeyNotFoundException)
            {
                return RedirectToAction("AccesDanied", "Auth");
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("AccesDanied", "Auth");
            }
        }

        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}
