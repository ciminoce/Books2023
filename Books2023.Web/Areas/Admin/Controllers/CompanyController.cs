using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            Company company = new Company();
            if (id == null || id == 0)
            {
                return View(company);

            }
            else
            {
                company = _unitOfWork.Companies.Get(c => c.Id == id);
                return View(company);

            }
        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }
            if (company.Id == 0)
            {
                _unitOfWork.Companies.Add(company);
                TempData["success"] = "Company added successfully";
            }
            else
            {
                _unitOfWork.Companies.Update(company);
                TempData["success"] = "Company updated successfully";

            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Companies.GetAll();
            return Json(new { data = companyList });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Not Found" });

            }
            var companyDelete = _unitOfWork.Companies.Get(p => p.Id == id);
            if (companyDelete == null)
            {
                return Json(new { success = false, message = "Company not found" });
            }
            try
            {
                _unitOfWork.Companies.Delete(companyDelete);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Company Deleted succesfully" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
    }
}
