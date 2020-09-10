using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BSOS.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        [Required]
        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name ="Selected Product")]
        public ICollection<Product> SelectedProduct { get; set; }

        [Required]
        public Customer Customer { get; set; }
    }
}
