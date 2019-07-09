using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApi.ViewModel
{
    public class ListCustomerVM
    {
        public string Fname { get; set; } 
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; } 
        public DateTime Created { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
