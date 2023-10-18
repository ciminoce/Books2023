using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Books2023.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork=unitOfWork;
        }

        public IActionResult Index()
        {
            var productList = _unitOfWork.Products.GetAll();
            return View(productList);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product=_unitOfWork.Products.Get(p=>p.Id == id,"Category,CoverType");
            ShoppingCart cart = new ShoppingCart
            {
                Product = product,
                Quantity = 1
            };
            return View(cart);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}