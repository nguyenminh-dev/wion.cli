using Xunit;

namespace Wion.Template.EntityFrameworkCore;

[CollectionDefinition(TemplateTestConsts.CollectionDefinitionName)]
public class TemplateEntityFrameworkCoreCollection : ICollectionFixture<TemplateEntityFrameworkCoreFixture>
{

}
