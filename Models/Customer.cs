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

        [StringLength(20)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName{get; set;}

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName{ get; set; }
        public string Gender { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^[0-9\s]*$")]
        public string PhoneNumber{ get; set; }

        [Required]
        [RegularExpression(@"^[@A-Za-z0-9\s]*$")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name="Zip Code")]
        [RegularExpression(@"^[0-9\s]*$")]
        public string ZipCode { get; set; }

        [Required]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public ICollection<Order> Orders  { get; set; }

    }
}
