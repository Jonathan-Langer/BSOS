using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BSOS.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name ="Selected Product")]
        public Product SelectedProduct { get; set; }
        public Client client { get; set; }
    }
}
