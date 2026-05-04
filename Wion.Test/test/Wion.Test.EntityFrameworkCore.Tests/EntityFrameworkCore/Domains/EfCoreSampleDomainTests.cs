using Wion.Test.Samples;
using Xunit;

namespace Wion.Test.EntityFrameworkCore.Domains;

[Collection(TestTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TestEntityFrameworkCoreTestModule>
{

}
