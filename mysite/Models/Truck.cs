using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Models
{
    public class Truck
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tech Name")]
        public string Name { get; set; }
        [Display(Name = "Please enter Truck Number")]
        public string TruckNumber { get; set; }
        [Display(Name = "Please enter Details")]
        public string Details { get; set; }
        [Display(Name = "Please Enter Category")]
        public string Category { get; set; }
        [Display(Name = "Please enter the link")]
        public string Link { get; set; }
        [Display(Name = "Please choose an image")]
        public string TruckPhoto { get; set; }
    }
}
