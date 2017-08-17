//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Samsonite.OMS.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public long Id { get; set; }
        public string GroupDesc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MatlGroup { get; set; }
        public string Material { get; set; }
        public string GdVal { get; set; }
        public string EAN { get; set; }
        public string SKU { get; set; }
        public decimal SupplyPrice { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public System.DateTime QuantityEditDate { get; set; }
        public string ProductId { get; set; }
        public bool IsCommon { get; set; }
        public bool IsSet { get; set; }
        public bool IsGift { get; set; }
        public bool IsDelete { get; set; }
        public System.DateTime AddDate { get; set; }
        public System.DateTime EditDate { get; set; }
    }
}
