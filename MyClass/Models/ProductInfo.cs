﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public int Catid { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public string Img { get; set; }        
        public Decimal Price { get; set; }
        public Decimal Pricesale { get; set; }
        public DateTime? Create_At { get; set; }        
        public int Status { get; set; }
        public string CategoryName { get; set; }
    }
}
