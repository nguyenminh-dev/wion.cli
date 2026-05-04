using Wion.Test.Samples;
using Xunit;

namespace Wion.Test.EntityFrameworkCore.Applications;

[Collection(TemplateTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TemplateEntityFrameworkCoreTestModule>
{

}
