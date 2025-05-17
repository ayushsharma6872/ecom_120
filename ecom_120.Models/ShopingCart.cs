using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string ApplicationuserId { get; set; }
        [ForeignKey("ApplicationuserId")]
        public Applicationuser Applicationuser { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Count { get; set; }
        [NotMapped]
        public double Price { get; set; }
        public bool IsSelected { get; set; } = true;
    }
}
