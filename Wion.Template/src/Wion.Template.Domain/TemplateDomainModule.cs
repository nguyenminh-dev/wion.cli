using Volo.Abp.Caching;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Wion.Template.MultiTenancy;

namespace Wion.Template;

[DependsOn(
    typeof(TemplateDomainSharedModule),
    typeof(AbpCachingModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpTenantManagementDomainModule)
    )]
public class TemplateDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });
    }
}
