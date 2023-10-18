using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categoryList = _uniOfWork.Categories.GetAll();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            if (_uniOfWork.Categories.Exists(category))
            {
                ModelState.AddModelError(string.Empty, "Category already exists!!!");
                return View(category);
            }
            _uniOfWork.Categories.Add(category);
            _uniOfWork.Save();
            TempData["success"] = "Record added successfully!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _uniOfWork.Categories.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            if (_uniOfWork.Categories.Exists(category))
            {
                ModelState.AddModelError(string.Empty, "Category already exists!!!");
                return View(category);
            }
            _uniOfWork.Categories.Update(category);
            _uniOfWork.Save();
            TempData["success"] = "Record updated successfully!!";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _uniOfWork.Categories.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = _uniOfWork.Categories.Get(c => c.Id == id);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "Category does not exist");
            }
            _uniOfWork.Categories.Delete(category);
            _uniOfWork.Save();
            TempData["success"] = "Record removed successfully!!";

            return RedirectToAction("Index");
        }

    }
}
