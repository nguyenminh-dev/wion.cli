using Volo.Abp.Modularity;

namespace Wion.Template;

[DependsOn(
    typeof(TemplateDomainModule),
    typeof(TemplateTestBaseModule)
)]
public class TemplateDomainTestModule : AbpModule
{

}
