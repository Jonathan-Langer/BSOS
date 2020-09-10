using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSOS.Models
{
    public class Customer
    {

        public Customer()
        {

        }


        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName{get; set;}
        [Display(Name = "Last Name")]
        public string LastName{ get; set; }
        public string Gender { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set;}
        public string Country { get; set; }
        public string ZipCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        
        


    }
}
