using CoreProject.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Seed.Host
{
    public class DefaultSourceCreator
    {
        private readonly WarrantyContext _context;

        public static List<Region> InitialSources => GetInitialSources();

        public DefaultSourceCreator(WarrantyContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSource();
        }

        private static List<Region> GetInitialSources()
        {
            return new List<Region>
            {
                new Region("CN","China"),
                new Region("HK","HongKong"),
                new Region("SGP","Singapore")
            };
        }

        private void CreateSource()
        {
            foreach (var region in InitialSources)
            {
                AddSourceIfNotExists(region);
            }
        }

        private void AddSourceIfNotExists(Region region)
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
