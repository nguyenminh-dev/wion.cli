using System.Threading.Tasks;
using Xunit;

namespace Wion.Test.EntityFrameworkCore.Samples;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * Only test your custom repository methods.
 */
[Collection(TemplateTestConsts.CollectionDefinitionName)]
public class SampleRepositoryTests : TemplateEntityFrameworkCoreTestBase
{
    public SampleRepositoryTests()
    {
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            //Act

            //Assert

        });
    }
}
