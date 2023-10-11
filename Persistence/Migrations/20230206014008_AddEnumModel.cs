using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobJuggler.Persistence.Migrations;

/// <inheritdoc />
public partial class AddEnumModel : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(File.ReadAllText(@"..\Persistence\Migrations\SQL\enums_view.sql"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql("DROP VIEW IF EXISTS crystal_clean.enums");
    }
}
