using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Models
{
    /// <summary>
    /// 保修者
    /// </summary>
    public class WarrantyOwner
    {
        public int WarrantyOwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreationDate { get; set; }
        public Boolean Enable { get; set; }

        /// <summary>
        /// 地区Id
        /// </summary>
        public int RegionId { get; set; }
        ///// <summary>
        ///// 地区
        ///// </summary>
        //public Region Region { get; set; }
        public List<WarrantyCard> WarrantyCards { get; set; }

    }
}
