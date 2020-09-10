using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BSOS.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name="Name")]
        public string ProductName { get; set; }
        public int Size { get; set; }
        public string Manufacturer { get; set; }
        public string Importer { get; set; }
        public string Color { get; set; }
    }
}
