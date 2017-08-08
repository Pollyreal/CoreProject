using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Models
{
    /// <summary>
    /// 商品保修信息
    /// </summary>
    public class ProductWarranty
    {
        public int ProductWarrantyId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// 保修时长
        /// </summary>
        public int WarrantyYear { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        
        public DateTime InitalDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Boolean Enable { get; set; }
    }
}
