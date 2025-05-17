using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.Models
{
    public class Company
    {
        public int id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Street Adreess")]
        public string StreetAddress { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "IS Authorized Company")]
        public bool IsAuhtorizedCompany { get; set; }

    }
}
