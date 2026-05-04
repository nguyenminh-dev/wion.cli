using Wion.Test.Localization;
using Volo.Abp.Application.Services;

namespace Wion.Test;

/* Inherit your application services from this class.
 */
public abstract class TestAppService : ApplicationService
{
    protected TestAppService()
    {
        LocalizationResource = typeof(TestResource);
    }
}
