using Volo.Abp.Modularity;

namespace Wion.Test;

public abstract class TemplateApplicationTestBase<TStartupModule> : TemplateTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
