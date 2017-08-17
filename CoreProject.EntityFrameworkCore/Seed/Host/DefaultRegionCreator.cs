using CoreProject.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Seed.Host
{
    public class DefaultRegionCreator
    {
        private readonly WarrantyContext _context;

        public static List<Region> InitialRegions => GetInitialRegions();

        public DefaultRegionCreator(WarrantyContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateRegion();
        }

        private static List<Region> GetInitialRegions()
        {
            return new List<Region>
            {
                new Region("CN","China"),
                new Region("HK","HongKong"),
                new Region("SGP","Singapore")
            };
        }

        private void CreateRegion()
        {
            foreach (var region in InitialRegions)
            {
                AddRegionIfNotExists(region);
            }
        }

        private void AddRegionIfNotExists(Region region)
        {
            if (_context.Regions.Any(r => r.RegionCode == region.RegionCode))
            {
                return;
            }

            _context.Regions.Add(region);

            _context.SaveChanges();
        }
    }
}
