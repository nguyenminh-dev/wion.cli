using Wion.Test.Samples;
using Xunit;

namespace Wion.Test.EntityFrameworkCore.Applications;

[Collection(TestTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TestEntityFrameworkCoreTestModule>
{

}
