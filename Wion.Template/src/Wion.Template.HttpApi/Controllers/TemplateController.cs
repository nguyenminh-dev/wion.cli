using Wion.Template.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Wion.Template.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TemplateController : AbpControllerBase
{
    protected TemplateController()
    {
        LocalizationResource = typeof(TemplateResource);
    }
}
