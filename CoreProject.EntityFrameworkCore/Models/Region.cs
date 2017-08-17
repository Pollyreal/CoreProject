using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Models
{
    public class Region
    {
        [Key]
        public string RegionCode { get; set; }
        public string RegionName { get; set; }

        public Region(string RegionCode,string RegionName)
        {
            this.RegionCode = RegionCode;
            this.RegionName = RegionName;
        }
    }
}
