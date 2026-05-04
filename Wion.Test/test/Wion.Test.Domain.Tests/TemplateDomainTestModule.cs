using Volo.Abp.Modularity;

namespace Wion.Test;

[DependsOn(
    typeof(TemplateDomainModule),
    typeof(TemplateTestBaseModule)
)]
public class TemplateDomainTestModule : AbpModule
{

}
