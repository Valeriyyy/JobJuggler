using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobJuggler.Persistence.Migrations;

public partial class JobInvoices : Migration {
    const string _schemaName = "main";
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropForeignKey(
            name: "invoice_job_id_foreign",
            schema: _schemaName,
            table: "jobs");

        migrationBuilder.AlterColumn<bool>(
            name: "is_paid",
            schema: _schemaName,
            table: "invoices",
            type: "boolean",
            nullable: false,
            defaultValue: false,
            comment: "Indicates if the invoice has been fully paid for",
            oldClrType: typeof(bool),
            oldType: "boolean",
            oldComment: "Indicates if the invoice has been fully paid for");

        migrationBuilder.CreateIndex(
            name: "IX_invoices_job_id",
            schema: _schemaName,
            table: "invoices",
            column: "job_id",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "job_invoice_id_foreign",
            schema: _schemaName,
            table: "invoices",
            column: "job_id",
            principalSchema: _schemaName,
            principalTable: "jobs",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropForeignKey(
            name: "job_invoice_id_foreign",
            schema: _schemaName,
            table: "invoices");

        migrationBuilder.DropIndex(
            name: "IX_invoices_job_id",
            schema: _schemaName,
            table: "invoices");

        migrationBuilder.AlterColumn<bool>(
            name: "is_paid",
            schema: _schemaName,
            table: "invoices",
            type: "boolean",
            nullable: false,
            comment: "Indicates if the invoice has been fully paid for",
            oldClrType: typeof(bool),
            oldType: "boolean",
            oldDefaultValue: false,
            oldComment: "Indicates if the invoice has been fully paid for");

        migrationBuilder.AddForeignKey(
            name: "invoice_job_id_foreign",
            schema: _schemaName,
            table: "jobs",
            column: "id",
            principalSchema: _schemaName,
            principalTable: "invoices",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
