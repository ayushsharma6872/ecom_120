using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderHeaderid { get; set; }
        [ForeignKey("OrderHeaderid")]
        public OrderHeader OrderHeader { get; set; }
        public int Productid { get; set; }
        [ForeignKey("Productid")]
        public Product Product { get; set; }
        public int Count    { get; set; }
        public int Price { get; set; }
    }
}
