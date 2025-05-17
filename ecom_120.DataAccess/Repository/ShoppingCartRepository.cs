using ecom_120.DataAccess.Data;
using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.DataAccess.Repository
{
    public class ShoppingCartRepository:Repository<ShoppingCart>,IShoppingCart
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository (ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
