using CoreProject.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace CoreProject.EntityFrameworkCore
{
    public class TemporaryDbContextFactory: IDbContextFactory<BloggingContext>
    {
        public BloggingContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<BloggingContext>();

            builder.UseSqlServer("Server=WLCNNB50703020\\SQLEXPRESS; Database=CoreProjectDb; Trusted_Connection=True;",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(BloggingContext).GetTypeInfo().Assembly.GetName().Name));

            return new BloggingContext(builder.Options);
        }
    }
}
