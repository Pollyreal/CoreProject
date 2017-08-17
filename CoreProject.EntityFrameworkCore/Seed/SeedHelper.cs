using CoreProject.EntityFrameworkCore.Seed.Host;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(WarrantyContext context)
        {
            // Host seed
            new InitialHostDbBuilder(context).Create();
        }
    }
}
