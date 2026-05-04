using System.Threading.Tasks;

namespace Wion.Template.Data;

public interface ITemplateDbSchemaMigrator
{
    Task MigrateAsync();
}
