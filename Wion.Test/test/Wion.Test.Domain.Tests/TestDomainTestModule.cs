using Volo.Abp.Modularity;

namespace Wion.Test;

[DependsOn(
    typeof(TestDomainModule),
    typeof(TestTestBaseModule)
)]
public class TestDomainTestModule : AbpModule
{

}
