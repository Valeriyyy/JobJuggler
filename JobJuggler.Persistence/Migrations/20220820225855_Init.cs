using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobJuggler.Persistence.Migrations;

public partial class Init : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        // Create the main schema
        migrationBuilder.Sql(File.ReadAllText(@"../JobJuggler.Persistence/Migrations/SQL/create_main_schema.sql"));

        // Ensure the main schema exists
        migrationBuilder.EnsureSchema(
            name: "main");

        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

        migrationBuilder.Sql(File.ReadAllText(@"../JobJuggler.Persistence/Migrations/SQL/guid_trigger_func.sql"));
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql("DROP FUNCTION main.guid_trigger_func();");
    }
}
