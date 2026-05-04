using Wion.Test.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Wion.Test.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TemplateEntityFrameworkCoreModule),
    typeof(TemplateApplicationContractsModule)
)]
public class TemplateDbMigratorModule : AbpModule
{
}
