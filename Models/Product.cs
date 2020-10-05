using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSOS.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        public string Size { get; set; }
        public string Brand { get; set; } //The Manufacturer of the product
        public string Color { get; set; }
        public string Discription { get; set; }
        public string Urlimage { get; set; }

        public string Category { get; set; }  //The category that the product belongs to. For example: Shirt,Pants,Shoes...
                                               //may contain also gender (not necessary) and also multi categories
        public ICollection<ProductOrder> ProductOrders { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}