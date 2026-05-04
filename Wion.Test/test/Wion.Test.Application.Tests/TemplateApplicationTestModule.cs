using Volo.Abp.Modularity;

namespace Wion.Test;

[DependsOn(
    typeof(TemplateApplicationModule),
    typeof(TemplateDomainTestModule)
)]
public class TemplateApplicationTestModule : AbpModule
{

}
