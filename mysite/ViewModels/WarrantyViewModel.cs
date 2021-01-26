using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.ViewModels
{
    public class WarrantyViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Company Details")]
        public string Details { get; set; }
        [Required]
        [Display(Name = "Company Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
