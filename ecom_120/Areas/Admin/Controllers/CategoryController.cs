using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Models;
using ecom_120.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecom_120.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CategoryController : Controller
    {
        private readonly IunitofWork _unitofWork;

        public CategoryController(IunitofWork unitofWork)
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
            var categoryList = _unitofWork.Category.GetAll();
            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoryInDb = _unitofWork.Category.Get(id);
            if (categoryInDb == null)
                return Json(new { success = false, message = "Something went wrong!" });

            _unitofWork.Category.Remove(categoryInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data deleted successfully" });
        }

        #endregion

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                // Create
                return View(category);
            }

            // Edit
            category = _unitofWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (category == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(category);

            if (category.id == 0)
            {
                _unitofWork.Category.Add(category);
            }
            else
            {
                _unitofWork.Category.Update(category);
            }

            _unitofWork.Save();
            return RedirectToAction("Index");
        }
    }
}
