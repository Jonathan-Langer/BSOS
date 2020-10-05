using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name ="Total Price")]
        public double TotalPrice { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } //M2M
        public Customer Customer { get; set; }

        [Display(Name ="Customer Id")]
        public int CustomerId { get; set; }
        
    }
}
