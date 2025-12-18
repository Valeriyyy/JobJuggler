using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedDefaultDateCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "job_juggler",
                table: "subscriptions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() at time zone 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "job_juggler",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() at time zone 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "job_juggler",
                table: "contacts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() at time zone 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "identity",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() at time zone 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "job_juggler",
                table: "subscriptions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now() at time zone 'UTC'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "job_juggler",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now() at time zone 'UTC'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "job_juggler",
                table: "contacts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now() at time zone 'UTC'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_created",
                schema: "identity",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now() at time zone 'UTC'");
        }
    }
}
