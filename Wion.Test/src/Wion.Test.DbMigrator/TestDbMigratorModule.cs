using Wion.Test.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Wion.Test.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TestEntityFrameworkCoreModule),
    typeof(TestApplicationContractsModule)
)]
public class TestDbMigratorModule : AbpModule
{
}
