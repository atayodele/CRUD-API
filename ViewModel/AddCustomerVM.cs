using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApi.ViewModel
{
    public class AddCustomerVM
    {
        public AddCustomerVM()
        {
            Created = DateTime.Now;
        }
        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Firstname must be btw 4 and 10 characters")]
        public string Fname { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Lastname must be btw 4 and 10 characters")]
        public string Lname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
