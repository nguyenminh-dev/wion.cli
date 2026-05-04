using System.Threading.Tasks;

namespace Wion.Test.Data;

public interface ITemplateDbSchemaMigrator
{
    Task MigrateAsync();
}
