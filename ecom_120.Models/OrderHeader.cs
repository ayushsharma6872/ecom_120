using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationuserId { get; set; }
        public Applicationuser Applicationuser { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public Double OrderTotal { get; set; }
        
        public string? TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string TransactionId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Street Adreess")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
