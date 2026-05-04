using Xunit;

namespace Wion.Test.EntityFrameworkCore;

[CollectionDefinition(TemplateTestConsts.CollectionDefinitionName)]
public class TemplateEntityFrameworkCoreCollection : ICollectionFixture<TemplateEntityFrameworkCoreFixture>
{

}
