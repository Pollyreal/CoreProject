using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly WarrantyContext _context;
        public InitialHostDbBuilder(WarrantyContext context)
        {
            _context = context;
        }
        public void Create()
        {
            new DefaultRegionCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
