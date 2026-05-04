using Volo.Abp.Modularity;

namespace Wion.Test;

[DependsOn(
    typeof(TestApplicationModule),
    typeof(TestDomainTestModule)
)]
public class TestApplicationTestModule : AbpModule
{

}
