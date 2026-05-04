using Volo.Abp.Modularity;

namespace Wion.Test;

public abstract class TestApplicationTestBase<TStartupModule> : TestTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
