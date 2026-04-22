using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Wion.Template.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class TemplateDbContextFactory : IDesignTimeDbContextFactory<TemplateDbContext>
{
    public TemplateDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        TemplateEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<TemplateDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new TemplateDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Wion.Template.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
