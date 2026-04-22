using Volo.Abp.Modularity;

namespace Wion.Template
{
    [DependsOn(
        typeof(WionTemplateDomainSharedModule)
    )]
    public class WionTemplateDomainModule : AbpModule
    {
    }
}