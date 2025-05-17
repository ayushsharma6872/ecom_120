
using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Models;
using ecom_120.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace bookstore_110.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CoverTypeController : Controller
    {
        private readonly IunitofWork _unitofWork;
        public CoverTypeController(IunitofWork unitofWork)
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
            var coverTypeList = _unitofWork.CoverType.GetAll();
            return Json(new { data = coverTypeList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var coverTypeInDb = _unitofWork.CoverType.Get(id);
            if (coverTypeInDb == null) return Json(new { success = false, message = "Something went wrong" });
            _unitofWork.CoverType.Remove(coverTypeInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }
        #endregion

        public IActionResult upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null) return View(coverType);
            coverType = _unitofWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null) return NotFound();
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult upsert(CoverType coverType)
        {
            if (coverType == null) return NotFound();
            if (!ModelState.IsValid) return View(coverType);

            if (coverType.id == 0)
                _unitofWork.CoverType.Add(coverType);
            else
                _unitofWork.CoverType.Update(coverType);

            _unitofWork.Save();
            return RedirectToAction("Index");
        }
    }
}
