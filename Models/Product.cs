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
        public double Price { get; set; }
        public string Size { get; set; }
        public string Manufacturer { get; set; }
        public string Importer { get; set; }
        public string Color { get; set; }
        public string Gender
        {
            get { return this.Gender; }
            set
            {
                if (value == "Men" || value == "Women" || value == "Children")
                    Gender = value;
                else
                    Gender = "Unisex";
            }
        }
    }
}
