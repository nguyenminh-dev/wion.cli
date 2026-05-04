using Wion.Test.Samples;
using Xunit;

namespace Wion.Test.EntityFrameworkCore.Domains;

[Collection(TemplateTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TemplateEntityFrameworkCoreTestModule>
{

}
