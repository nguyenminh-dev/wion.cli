using Wion.Template.Localization;
using Volo.Abp.Application.Services;

namespace Wion.Template;

/* Inherit your application services from this class.
 */
public abstract class TemplateAppService : ApplicationService
{
    protected TemplateAppService()
    {
        LocalizationResource = typeof(TemplateResource);
    }
}
