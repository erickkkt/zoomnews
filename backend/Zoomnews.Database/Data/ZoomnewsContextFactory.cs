using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zoomnews.Database.Data
{
    public class ZoomnewsContextFactory : IDesignTimeDbContextFactory<ZoomnewsDbContext>
    {
        public ZoomnewsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ZoomnewsDbContext>();

            //Should copy appsettings.json to bin folder 
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBConnection"));

            return new ZoomnewsDbContext(optionsBuilder.Options);
        }
    }
}
