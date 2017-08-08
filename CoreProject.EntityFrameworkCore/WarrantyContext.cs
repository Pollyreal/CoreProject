using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CoreProject.EntityFrameworkCore.Models;

namespace CoreProject.EntityFrameworkCore
{
    public class WarrantyContext : DbContext
    {
        public WarrantyContext(DbContextOptions<WarrantyContext> options)
            : base(options)
        { }
        public DbSet<WarrantyOwner> WarrantyOwners { get; set; }
        public DbSet<WarrantyCard> WarrantyCards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductWarranty> ProductWarranties { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}
