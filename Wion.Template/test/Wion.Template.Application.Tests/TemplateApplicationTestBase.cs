using Volo.Abp.Modularity;

namespace Wion.Template;

public abstract class TemplateApplicationTestBase<TStartupModule> : TemplateTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
