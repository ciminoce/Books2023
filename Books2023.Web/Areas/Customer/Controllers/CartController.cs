using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Books2023.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVm shoppingCart { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart = new ShoppingCartVm
            {
                CartList = _unitOfWork.ShoppingCarts
                .GetAll(c => c.ApplicationUserId == userId.Value,propertiesNames:"Product")
            };

            return View(shoppingCart);
        }

        public IActionResult Plus(int cartId)
        {
            var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
            _unitOfWork.ShoppingCarts.IncrementQuantity(cartInDb, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
		public IActionResult Minus(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
			_unitOfWork.ShoppingCarts.DecrementQuantity(cartInDb, 1);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

	}
}
