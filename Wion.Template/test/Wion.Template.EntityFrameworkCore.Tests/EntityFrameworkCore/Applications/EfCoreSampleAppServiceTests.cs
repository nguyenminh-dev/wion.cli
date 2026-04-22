using Wion.Template.Samples;
using Xunit;

namespace Wion.Template.EntityFrameworkCore.Applications;

[Collection(TemplateTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TemplateEntityFrameworkCoreTestModule>
{

}
