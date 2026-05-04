using Wion.Test.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Wion.Test.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TestController : AbpControllerBase
{
    protected TestController()
    {
        LocalizationResource = typeof(TestResource);
    }
}
