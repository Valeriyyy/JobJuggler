using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Migrations.Tools;

#nullable disable

namespace Persistence.Migrations;
public partial class AddClients : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var tableName = "clients";
        var schemaName = "crystal_clean";
        migrationBuilder.EnsureSchema(
            name: schemaName);

        migrationBuilder.CreateTable(
            name: tableName,
            schema: schemaName,
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                name = table.Column<string>(type: "text", nullable: false),
                phone = table.Column<string>(type: "text", nullable: true),
                email = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_clients", x => x.id);
            });

        migrationBuilder.AddGuidTriggerFunction(schemaName, tableName);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "clients",
            schema: "crystal_clean");
    }
}
