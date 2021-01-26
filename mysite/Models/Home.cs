using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Models
{
    public class Home
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Name { get; set; } = "";

        public string Service { get; set; } = "";

        public string Description { get; set; } = "";

        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = ""; 

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
