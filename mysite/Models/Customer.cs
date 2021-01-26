using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Models
{
    public class Customer
    {
        [Display(Name ="Invoice Number")]
        public int Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        [Display(Name = "Please Enter First Name")]
        public string CustomerFirst { get; set; }

        [Display(Name = "Please Enter Last Name")]
        public string CustomerLast { get; set; }

        [Display(Name = "Please Enter Last Name")]
        public string Email { get; set; }

        [Display(Name ="Please Enter Phone Number")]
        public string MobilPhone { get; set; }

        [Display(Name ="Please Enter Address")]
        public string BillingAddress { get; set; }

        [Display(Name ="Please Enter City")]
        public string City { get; set; }

        [Display(Name ="Please Enter State")]
        public string State { get; set; }

        [Display(Name ="Please Enter Zip Code")]
        public string PostalCode { get; set; }

        [Display(Name ="Please Enter Work order")]
        public string WorkOrder { get; set; }

        [Display(Name = "Please choose Model image")]
        public string ModelImage { get; set; }
    }
}
