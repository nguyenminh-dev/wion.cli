using Wion.Template.Samples;
using Xunit;

namespace Wion.Template.EntityFrameworkCore.Domains;

[Collection(TemplateTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TemplateEntityFrameworkCoreTestModule>
{

}
