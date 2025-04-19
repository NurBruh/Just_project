using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Just_project.Models
{
    public class LocalizeDbContextFactory : IDesignTimeDbContextFactory<LocalizeDbContext>
    {
        public LocalizeDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LocalizeDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)));

            return new LocalizeDbContext(builder.Options);
        }
    }
}
