using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using Persistence.Migrations.Tools;

#nullable disable

namespace Persistence.Migrations;

public partial class AddLocations : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {

        var schemaName = "crystal_clean";
        var tableName = "locations";
        migrationBuilder.CreateTable(
            name: tableName,
            schema: schemaName,
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                location_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                street1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                street2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                city = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                state = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                postal_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                gate_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                latitude = table.Column<decimal>(type: "numeric(15,7)", precision: 15, scale: 7, nullable: false),
                longitude = table.Column<decimal>(type: "numeric(15,7)", precision: 15, scale: 7, nullable: false),
                notes = table.Column<string>(type: "text", nullable: true, defaultValueSql: "''::text"),
                vector_address = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false, computedColumnSql: "\r                         to_tsvector('english', \r                         ((((CASE    \r                                 WHEN (street1 IS NOT NULL) THEN ((street1)::text || ' '::text)    \r                                 ELSE ''::text\r                         END || CASE    \r                                 WHEN (city IS NOT NULL) THEN ((city)::text || ' '::text)    \r                                 ELSE ''::text END) || CASE    WHEN (state IS NOT NULL) THEN ((state)::text || ' '::text)     ELSE ''::text END) || CASE \r                         WHEN (postal_code IS NOT NULL) THEN ((postal_code)::text || ' '::text)    ELSE ''::text END) || (CASE    WHEN (country IS NOT NULL) THEN country    \r                         ELSE ''::character varying END)::text))", stored: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_locations", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "location_guid_unique",
            schema: schemaName,
            table: tableName,
            column: "guid",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "locations_vector_address_idx",
            schema: schemaName,
            table: tableName,
            column: "vector_address")
            .Annotation("Npgsql:IndexMethod", "GIN");

        migrationBuilder.AddGuidTriggerFunction(schemaName, tableName);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "locations",
            schema: "crystal_clean");
    }
}
