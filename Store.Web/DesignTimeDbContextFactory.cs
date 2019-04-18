using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Store.Data.EntityFrameworkCore;

namespace Store.Web
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EfCoreApplicationDbContext>
    {
        public EfCoreApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<EfCoreApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("Default");
            builder.UseSqlServer(connectionString);
            return new EfCoreApplicationDbContext(builder.Options);
        }
    }
}
