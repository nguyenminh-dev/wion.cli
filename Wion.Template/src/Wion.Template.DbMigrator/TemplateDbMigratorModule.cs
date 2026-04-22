using Wion.Template.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Wion.Template.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TemplateEntityFrameworkCoreModule),
    typeof(TemplateApplicationContractsModule)
)]
public class TemplateDbMigratorModule : AbpModule
{
}
