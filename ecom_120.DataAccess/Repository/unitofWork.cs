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
    public class unitofWork:IunitofWork
    {
        private readonly ApplicationDbContext _context;
        public unitofWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            CoverType = new CoverTypeRepository(context);
            Product= new ProductRepository(context);
            Application = new ApplicationuserRepository(context);
            Company = new CompanyRepository(context);
            ShoppingCart=new ShoppingCartRepository(context);
            OrderHeader=new OrderHeaderRepository(context);
            OrderDetail=new OrderDetailRepository(context);

            
        }

        public ICoverTypeRepository CoverType { get; private set; }

        public ICategoryRepository Category {  get; private set; }
        public IProductRepository Product { get; private set; }
        public IApplicationRepository Application { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCart ShoppingCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail {  get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
