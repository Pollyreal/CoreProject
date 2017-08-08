using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string EAN { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Materials { get; set; }
        public string Grid { get; set; }
        public DateTime CreationDate { get; set; }
        public Boolean Enable { get; set; }
        public List<ProductWarranty> ProductWarranties { get; set; }
    }
}
