using ecom_120.DataAccess.Repository;
using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Models;
using ecom_120.Models.ViewModels;
using ecom_120.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ecom_120.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class ProductController : Controller
    {
        private readonly IunitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IunitofWork iunitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = iunitofWork;
            _webHostEnvironment = webHostEnvironment;
                
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _unitofWork.Product.Get(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            // Delete associated image if it exists
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _unitofWork.Product.Remove(product);
            _unitofWork.Save();

            return Json(new { success = true, message = "Delete successful" });
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data=_unitofWork.Product.GetAll()});
        }
        #endregion
        public IActionResult upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitofWork.Category.GetAll().Select(Cl => new SelectListItem()
                {
                    Text = Cl.name,
                    Value = Cl.id.ToString()
                }),
                CoverTypeList = _unitofWork.CoverType.GetAll().Select(Cl => new SelectListItem()
                {
                    Text= Cl.name,
                    Value = Cl.id.ToString()
                })
            };
            if (id == null) return View(productVM);
            productVM.Product=_unitofWork.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null) return NotFound();
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            var files = HttpContext.Request.Form.Files;
            var webRootPath = _webHostEnvironment.WebRootPath;
            var uploadsFolder = Path.Combine(webRootPath, "images", "products");

            // ✅ Ensure uploads folder exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (files.Count > 0)
            {
                var file = files[0];
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var newImagePath = Path.Combine(uploadsFolder, fileName + extension);

                // ✅ Delete old image if updating
                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // ✅ Save new image
                using (var fileStream = new FileStream(newImagePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
            }
            else if (productVM.Product.id != 0)
            {
                // ✅ Preserve existing image if no new file is uploaded
                var existing = _unitofWork.Product.Get(productVM.Product.id);
                if (existing != null)
                {
                    productVM.Product.ImageUrl = existing.ImageUrl;
                }
            }

            // ✅ Model validation
            if (!ModelState.IsValid)
            {
                LoadDropdowns(productVM);
                return View(productVM);
            }

            // ✅ Add or Update
            if (productVM.Product.id == 0)
            {
                _unitofWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitofWork.Product.Update(productVM.Product);
            }

            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdowns(ProductVM productVM)
        {
            productVM.CategoryList = _unitofWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.name,
                Value = c.id.ToString()
            });

            productVM.CoverTypeList = _unitofWork.CoverType.GetAll().Select(c => new SelectListItem
            {
                Text = c.name,
                Value = c.id.ToString()
            });
        }
    }
}
