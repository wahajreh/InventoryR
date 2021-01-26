using mysite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.ViewModels
{
    public class IndexViewModel
    {
        public int PageNumber { get; set; }

        public bool NextPage { get; set; }

        public string Category { get; set; }
        public string Search { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<Truck> Trucks { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

    }
}
