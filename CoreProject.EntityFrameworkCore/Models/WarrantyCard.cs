using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Models
{
    /// <summary>
    /// 保修卡
    /// </summary>
    public class WarrantyCard
    {
        public int WarrantyCardId { get; set; }
        public int WarrantyOwnerId { get; set; }
        public WarrantyOwner WarrantyOwner { get; set; }

        public string EAN { get; set; }
        public string TransactionId { get; set; }
        /// <summary>
        /// 购买渠道(eShop, Demandware, store, etc )
        /// </summary>
        public string PurchaseSource { get; set; }
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// 注册途径(Demandware, store, etc)
        /// </summary>
        public string RegisterationSource { get; set; }
        /// <summary>
        /// 同意规则标识
        /// </summary>
        public Boolean PermitTandC { get; set; }
        /// <summary>
        /// 奖励保修时长
        /// </summary>
        public int BonusTime { get; set; }
        public DateTime CreationDate { get; set; }
        public Boolean Enable { get; set; }

        public int PurchaseRegionId { get; set; }
        ///// <summary>
        ///// 地区
        ///// </summary>
        //public Region Region { get; set; }
    }
}
