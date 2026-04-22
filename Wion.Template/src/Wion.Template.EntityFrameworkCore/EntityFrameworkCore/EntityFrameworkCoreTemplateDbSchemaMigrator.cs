using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wion.Template.Data;
using Volo.Abp.DependencyInjection;

namespace Wion.Template.EntityFrameworkCore;

public class EntityFrameworkCoreTemplateDbSchemaMigrator
    : ITemplateDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTemplateDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the TemplateDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TemplateDbContext>()
            .Database
            .MigrateAsync();
    }
}
