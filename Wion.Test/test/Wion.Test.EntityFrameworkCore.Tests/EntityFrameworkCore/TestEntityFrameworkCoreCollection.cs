using Xunit;

namespace Wion.Test.EntityFrameworkCore;

[CollectionDefinition(TestTestConsts.CollectionDefinitionName)]
public class TestEntityFrameworkCoreCollection : ICollectionFixture<TestEntityFrameworkCoreFixture>
{

}
