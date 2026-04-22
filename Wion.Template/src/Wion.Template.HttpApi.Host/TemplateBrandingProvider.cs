using Microsoft.Extensions.Localization;
using Wion.Template.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Wion.Template;

[Dependency(ReplaceServices = true)]
public class TemplateBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TemplateResource> _localizer;

    public TemplateBrandingProvider(IStringLocalizer<TemplateResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
