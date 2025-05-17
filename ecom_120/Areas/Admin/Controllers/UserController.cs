using ecom_120.DataAccess.Data;
using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Models;
using ecom_120.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ecom_120.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IunitofWork _unitofWork;
        private readonly ApplicationDbContext _context;

        public UserController(IunitofWork unitofWork, ApplicationDbContext context)
        {
            _unitofWork = unitofWork;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region APIs

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.Applicationusers.ToList(); // aspnetuser
            var roles = _context.Roles.ToList();               // aspnetrole
            var userRoles = _context.UserRoles.ToList();       // aspnetuserrole

            foreach (var user in userList)
            {
                var userRole = userRoles.FirstOrDefault(u => u.UserId == user.Id);
                if (userRole != null)
                {
                    var role = roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Role = role.Name;
                    }
                }

                if (user.CompanyId == null)
                {
                    user.Company = new Company { Name = string.Empty };
                }
                else
                {
                    var company = _unitofWork.Company.Get(user.CompanyId.Value);
                    user.Company = company ?? new Company { Name = string.Empty };
                }
            }

            // Remove the admin user from the list
            userList.RemoveAll(u => u.Role == SD.Role_Admin);

            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var userInDb = _context.Applicationusers.FirstOrDefault(u => u.Id == id);

            if (userInDb == null)
                return Json(new { success = false, message = "User not found" });

            bool isLocked;

            if (userInDb.LockoutEnd.HasValue && userInDb.LockoutEnd > DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100); // Lock for a long time
                isLocked = true;
            }

            _context.SaveChanges();

            return Json(new
            {
                success = true,
                message = isLocked ? "User Locked" : "User Unlocked"
            });
        }

        #endregion
    }
}
