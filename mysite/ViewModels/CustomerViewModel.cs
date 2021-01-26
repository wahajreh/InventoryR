using Microsoft.AspNetCore.Http;
using mysite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Please Enter First Name")]
        public string CustomerFirst { get; set; }

        [Display(Name = "Please Enter Last Name")]
        public string CustomerLast { get; set; }

        [Display(Name = "Please Enter Last Name")]
        public string Email { get; set; }

        [Display(Name = "Please Enter Phone Number")]
        public string MobilPhone { get; set; }

        [Display(Name = "Please Enter Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "Please Enter City")]
        public string City { get; set; }

        [Display(Name = "Please Enter State")]
        public string State { get; set; }

        [Display(Name = "Please Enter Zip Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Please Enter Work order")]
        public string WorkOrder { get; set; }

        public string CurrentImage { get; set; }

        [Display(Name = "Please choose an image")]
        public IFormFile ModelImage { get; set; }
    }
}
