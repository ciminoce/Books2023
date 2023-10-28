using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Models;
using Books2023.Utilities;
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
		public ShoppingCartVm shoppingCartVm { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Product"),
				OrderHeader=new()
			
			};

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Price = GetPriceBasedOnQuantity(itemCart.Quantity,
					itemCart.Product.Price,
					itemCart.Product.Price50,
					itemCart.Product.Price100);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Quantity * itemCart.Price;
			}

			return View(shoppingCartVm);
		}

		[HttpGet]
		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Product"),
				OrderHeader=new()
			};
			shoppingCartVm.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUsers.Get(u => u.Id == userId.Value);


			shoppingCartVm.OrderHeader.Name = shoppingCartVm.OrderHeader.ApplicationUser.Name;
			shoppingCartVm.OrderHeader.StreetAddress = shoppingCartVm.OrderHeader.ApplicationUser.StreetAddress;
			shoppingCartVm.OrderHeader.City = shoppingCartVm.OrderHeader.ApplicationUser.City;
			shoppingCartVm.OrderHeader.State = shoppingCartVm.OrderHeader.ApplicationUser.State;
			shoppingCartVm.OrderHeader.PhoneNumber = shoppingCartVm.OrderHeader.ApplicationUser.PhoneNumber;
			shoppingCartVm.OrderHeader.ZipCode = shoppingCartVm.OrderHeader.ApplicationUser.ZipCode;
			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Price = GetPriceBasedOnQuantity(itemCart.Quantity,
					itemCart.Product.Price,
					itemCart.Product.Price50,
					itemCart.Product.Price100);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Quantity * itemCart.Price;
			}

			//return View(shoppingCart);
			return View(shoppingCartVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			shoppingCartVm.CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Product");

			shoppingCartVm.OrderHeader.PaymentStatus = WC.PaymentStatusPending;
			shoppingCartVm.OrderHeader.OrderStatus = WC.StatusPending;
			shoppingCartVm.OrderHeader.OrderDate = DateTime.Now;
			shoppingCartVm.OrderHeader.ApplicationUserId = userId.Value;

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Price = GetPriceBasedOnQuantity(itemCart.Quantity,
					itemCart.Product.Price,
					itemCart.Product.Price50,
					itemCart.Product.Price100);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Price * itemCart.Quantity;
			}

			try
			{

			}
			catch (Exception)
			{

				throw;
			}

		}


		private double GetPriceBasedOnQuantity(int quantity, double price, double price50, double price100)
		{
			if (quantity<=50)
			{
				return price;
			}else if (quantity <= 100)
			{
				return price50;
			}
			else
			{
				return price100;
			}
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
			if (cartInDb.Quantity == 1)
			{
				_unitOfWork.ShoppingCarts.Delete(cartInDb);
			}
			else
			{
				_unitOfWork.ShoppingCarts.DecrementQuantity(cartInDb, 1);

			}
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

		public IActionResult RemoveFromCart(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
			_unitOfWork.ShoppingCarts.Delete(cartInDb);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}



		#region API CALLS
		[HttpDelete]
		public IActionResult Delete(int id)
		{

			try
			{
				var cart = _unitOfWork.ShoppingCarts.Get(c => c.Id == id);
				_unitOfWork.ShoppingCarts.Delete(cart);
				_unitOfWork.Save();
				return Json(new { success = true, message = "Cart Removed Satisfactory" });

			}
			catch (Exception)
			{

				return Json(new { success = false, message = "Problems while trying to remove a cart" });

			}
		}

		[HttpPost]
		public IActionResult GetTotal()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			var shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(s => s.ApplicationUserId == claims.Value,propertiesNames:"Product")
			};
			foreach (var item in shoppingCartVm.CartList)
			{
				item.Price = GetPriceBasedOnQuantity(item.Quantity,
					item.Product.Price,
					item.Product.Price50,
					item.Product.Price100);
				shoppingCartVm.OrderHeader.OrderTotal += item.Price * item.Quantity;
			}
			return Json(new { total = shoppingCartVm.OrderHeader.OrderTotal });
		}
		#endregion
	}
}



