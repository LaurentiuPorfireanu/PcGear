using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using PcGear.Infrastructure.Config;

namespace PcGear.Database.Context
{
    public class PcGearDatabaseContextFactory : IDesignTimeDbContextFactory<PcGearDatabaseContext>
    {
        public PcGearDatabaseContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json");

            var configuration = builder.Build();
            AppConfig.Init(configuration);

            return new PcGearDatabaseContext();
        }
    }
}
