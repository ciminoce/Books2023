using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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

        public IActionResult Details(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return NotFound();
            }
            var product=_unitOfWork.Products.Get(p=>p.Id == productId,"Category,CoverType");
            ShoppingCart cart = new ShoppingCart
            {
                ProductId = product.Id,
                Product = product,
                Quantity = 1
            };
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimsIdentity =(ClaimsIdentity) User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = userId.Value;
            var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.ApplicationUserId == userId.Value
                        && c.ProductId == cart.ProductId);
            if (cartInDb == null)
            {
                _unitOfWork.ShoppingCarts.Add(cart);

            }
            else
            {
                _unitOfWork.ShoppingCarts.IncrementQuantity(cartInDb, cart.Quantity);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
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