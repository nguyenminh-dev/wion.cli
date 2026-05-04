using Volo.Abp.Modularity;

namespace Wion.Template;

[DependsOn(
    typeof(TemplateApplicationModule),
    typeof(TemplateDomainTestModule)
)]
public class TemplateApplicationTestModule : AbpModule
{

}
