using ecom_120.DataAccess.Repository;
using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Models;
using ecom_120.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecom_120.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IunitofWork _unitofWork;
        public CompanyController(IunitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyList = _unitofWork.Company.GetAll();
            return Json(new { data = CompanyList });
        }

        
        public IActionResult Delete(int id)
        {
            var companyIndb = _unitofWork.Company.Get(id);
            if (companyIndb == null)
                return Json(new { success = false, message = "Something went wrong while delete data!!" });
            _unitofWork.Company.Remove(companyIndb);
            _unitofWork.Save();
            return Json(new { success = true, Message = "Data deleted successfully!!" });
        }
        #endregion
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null) return View(company);
            company = _unitofWork.Company.Get(id.GetValueOrDefault());
            if (company == null) return NotFound();
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (company == null) return BadRequest();
            if (!ModelState.IsValid) return View(company);
            if (company.id == 0)
                _unitofWork.Company.Add(company);
            else
                _unitofWork.Company.Update(company);
            _unitofWork.Save();
            return RedirectToAction("Index");
        }
    }
}
