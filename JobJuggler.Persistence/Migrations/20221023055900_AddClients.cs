using JobJuggler.Persistence.Migrations.Tools;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations;

public partial class AddClients : Migration {
    const string _schemaName = "main";
    const string _tableName = "clients";
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.EnsureSchema(
            name: _schemaName);

        migrationBuilder.AlterDatabase()
            .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

        migrationBuilder.CreateTable(
            name: _tableName,
            schema: _schemaName,
            columns: table => new {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                name = table.Column<string>(type: "text", nullable: false),
                phone = table.Column<string>(type: "text", nullable: false),
                email = table.Column<string>(type: "text", nullable: true, defaultValue: null)
            },
            constraints: table => {
                table.PrimaryKey("PK_clients", x => x.id);
            });

        migrationBuilder.AddGuidTriggerFunction(_schemaName, _tableName);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "clients",
            schema: _schemaName);

        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
    }
}
