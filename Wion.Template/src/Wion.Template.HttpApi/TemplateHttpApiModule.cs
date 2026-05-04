using Localization.Resources.AbpUi;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Wion.Template.Localization;

namespace Wion.Template;

[DependsOn(
    typeof(TemplateApplicationContractsModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule)
    )]
public class TemplateHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TemplateResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
