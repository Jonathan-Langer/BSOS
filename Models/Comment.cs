﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSOS.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Za-z0-9\s]$")]
        [StringLength(50)]
        public string Title { get; set; }

        [RegularExpression(@"^[A-Za-z0-9\s]$")]
        [StringLength(200)]
        public string Body { get; set; }

        [RegularExpression(@"^[A-Za-z0-9\s]*$")]
        [StringLength(30)]
        public string SentBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Posted { get; set; }
        public string IP { get; set; } //IP Adress
        public string UrlProfile { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}