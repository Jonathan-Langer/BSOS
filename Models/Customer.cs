using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace BSOS.Models
{
    public enum Role
    {
        Admin,
        Customer
    }
    public class Customer 
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; }
        public string Gender { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        //[Index(IsUnique = true)]

        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        [RegularExpression(@"^[0-9\s]*$")]
        [StringLength(7)]
        public string ZipCode { get; set; }

        [Required]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public ICollection<Order> Orders { get; set; }//Order history

        public Role Role { get; set; }
        public static Stack<int> CustomersId { get; set; }

    }
}
