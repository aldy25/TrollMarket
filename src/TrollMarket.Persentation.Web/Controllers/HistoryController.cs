using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrollMarket.Persentation.Web.Services;

namespace TrollMarket.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        private readonly HistoryService _historyService;
        private readonly ILogger<HistoryController> _logger;

        public HistoryController(HistoryService historyService, ILogger<HistoryController> logger)
        {
            _historyService = historyService;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string? buyerNumber, string? sellerNumber, int page = 1, int pageSize = 10)
        {
            var vm = _historyService.GetAll(page,pageSize,buyerNumber,sellerNumber);
            return View(vm);
        }
        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}
