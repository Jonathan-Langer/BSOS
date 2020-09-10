﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BSOS.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name="Name")]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        
        [Required]
        public ICollection<string> Sizes { get; set; }
        public string Brand { get; set; } //The Manufacturer of the product
        public string Color { get; set; }

        [Required]
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

        [Required]
        public ICollection<Order> Orders { get; set; }
    }
}
