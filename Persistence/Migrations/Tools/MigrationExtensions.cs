using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.Tools;

public static class MigrationExtensions
{
    public static MigrationBuilder AddGuidTriggerFunction(this MigrationBuilder migrationBuilder, string schemaName, string tableName)
    {
        var triggerName = $"crystal_clean_{tableName}";
        var fullTableName = $"{schemaName}.{tableName}";
        migrationBuilder.Sql($@"
            CREATE TRIGGER {triggerName}
                BEFORE INSERT OR UPDATE
                ON {fullTableName}
                FOR EACH ROW
                EXECUTE FUNCTION crystal_clean.guid_trigger_func();

            COMMENT ON TRIGGER {triggerName} ON {fullTableName}
                IS 'Prevents users from updating the guid field';
            ");
        return migrationBuilder;
    }
}
