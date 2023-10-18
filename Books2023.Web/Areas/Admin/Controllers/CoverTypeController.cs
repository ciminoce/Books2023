using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var coverTypesList = _uniOfWork.CoverTypes.GetAll();
            return View(coverTypesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(coverTypes);
            }
            if (_uniOfWork.CoverTypes.Exists(coverTypes))
            {
                ModelState.AddModelError(string.Empty, "Cover Type already exists!!!");
                return View(coverTypes);
            }
            _uniOfWork.CoverTypes.Add(coverTypes);
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
            var coverTypes = _uniOfWork.CoverTypes.Get(c => c.Id == id);
            if (coverTypes == null)
            {
                return NotFound();
            }
            return View(coverTypes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(coverTypes);
            }
            if (_uniOfWork.CoverTypes.Exists(coverTypes))
            {
                ModelState.AddModelError(string.Empty, "Cover Type already exists!!!");
                return View(coverTypes);
            }
            _uniOfWork.CoverTypes.Update(coverTypes);
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
            var coverTypes = _uniOfWork.CoverTypes.Get(c => c.Id == id);
            if (coverTypes == null)
            {
                return NotFound();
            }
            return View(coverTypes);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var coverTypes = _uniOfWork.CoverTypes.Get(c => c.Id == id);
            if (coverTypes == null)
            {
                ModelState.AddModelError(string.Empty, "CoverTypes does not exist");
            }
            _uniOfWork.CoverTypes.Delete(coverTypes);
            _uniOfWork.Save();
            TempData["success"] = "Record removed successfully!!";

            return RedirectToAction("Index");
        }

    }
}
