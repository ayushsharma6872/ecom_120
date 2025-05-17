using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.DataAccess.Repository.IRepository
{
    public interface IunitofWork
    {
        ICoverTypeRepository CoverType { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IApplicationRepository Application { get; }
        ICompanyRepository Company { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IShoppingCart ShoppingCart { get; }
        

        void Save();
    }
}
