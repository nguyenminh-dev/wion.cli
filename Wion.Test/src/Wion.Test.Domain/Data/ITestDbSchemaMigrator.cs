using System.Threading.Tasks;

namespace Wion.Test.Data;

public interface ITestDbSchemaMigrator
{
    Task MigrateAsync();
}
