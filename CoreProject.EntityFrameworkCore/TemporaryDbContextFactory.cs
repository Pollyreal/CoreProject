using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace CoreProject.EntityFrameworkCore
{
    public class TemporaryDbContextFactory: IDbContextFactory<WarrantyContext>
    {
        public WarrantyContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<WarrantyContext>();

            builder.UseSqlServer("Server=WLCNNB50703020\\SQLEXPRESS; Database=CoreProjectDb; Trusted_Connection=True;",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(WarrantyContext).GetTypeInfo().Assembly.GetName().Name));

            return new WarrantyContext(builder.Options);
        }
    }
}
