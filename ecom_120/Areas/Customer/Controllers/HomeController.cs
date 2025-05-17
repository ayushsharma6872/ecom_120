using System.Diagnostics;
using System.Security.Claims;
using ecom_120.DataAccess.Data;
using ecom_120.DataAccess.Migrations;
using ecom_120.DataAccess.Repository;
using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Models;
using ecom_120.Utility;
using ecom_120.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecom_120.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IunitofWork _unitofWork;

        public HomeController(ILogger<HomeController> logger, IunitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claims != null)
            {
                var count = _unitofWork.ShoppingCart
                    .GetAll(sc => sc.ApplicationuserId == claims.Value)
                    .Count();

                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count);
            }

            var productList = _unitofWork.Product.GetAll();
            return View(productList);
        }


        public IActionResult Details(int id)
        {
            var productInDb = _unitofWork.Product.
                FirstorDefault(p=>p.id == id,includeProperties:"Category,CoverType");
            if (productInDb == null) return NotFound();
            var shoppingCart = new ShoppingCart()
            {
                Product = productInDb,
                ProductId = productInDb.id,
            };
            return View(shoppingCart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public IActionResult Details (ShoppingCart shoppingCart)
        {
            shoppingCart.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity=(ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if(claims==null) return NotFound();
                shoppingCart.ApplicationuserId = claims.Value;

                var shoppingCartInDb = _unitofWork.ShoppingCart.FirstorDefault
                    (sc => sc.ApplicationuserId == claims.Value &&
                    sc.ProductId == shoppingCart.ProductId);
                if (shoppingCartInDb == null)
                    _unitofWork.ShoppingCart.Add(shoppingCart);
                else
                    shoppingCartInDb.Count += shoppingCart.Count;
                _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var productInDb = _unitofWork.Product.FirstorDefault
                    (p => p.id == shoppingCart.Id, includeProperties: "Category,CoverType");
                if(productInDb == null) return NotFound();
                var shoppingCartEdit = new ShoppingCart()
                {
                    Product = productInDb,
                    ProductId = productInDb.id,
                };
                return View(shoppingCartEdit);
            }
               
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
