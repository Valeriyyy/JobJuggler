using Domain.Models.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Migrations.Tools;

#nullable disable

namespace Persistence.Migrations;

public partial class AddFinances : Migration
{
    private const string _schemaName = "crystal_clean";
    private const string _lineItemsTableName = "line_items";
    private const string _paymentMethodsTableName = "payment_methods";
    private const string _invoicesTableName = "invoices";
    private const string _invoiceLinesTableName = "invoice_lines";
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:Enum:crystal_clean.price_type", "none,per_unit,flat_rate")
            .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
            .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

        migrationBuilder.CreateTable(
            name: _lineItemsTableName,
            schema: _schemaName,
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                name = table.Column<string>(type: "text", nullable: false),
                base_price = table.Column<decimal>(type: "numeric", nullable: true, comment: "The default price for the item. Can be overridden when put on an invoice"),
                price_type = table.Column<PriceType>(type: $"{_schemaName}.price_type", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_line_items", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: _paymentMethodsTableName,
            schema: _schemaName,
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                name = table.Column<string>(type: "text", nullable: false, comment: "The name of the payment method used"),
                is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Indicates if the payment method is still meant to be used")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_payment_methods", x => x.id);
            });


        migrationBuilder.CreateTable(
            name: _invoicesTableName,
            schema: _schemaName,
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                job_id = table.Column<int>(type: "integer", nullable: false),
                consignee_id = table.Column<int>(type: "integer", nullable: false),
                reference_number = table.Column<string>(type: "text", nullable: true),
                total_price = table.Column<decimal>(type: "numeric", nullable: false, comment: "The calculated total from the invoice lines. Not meant to be directly edited"),
                payment_method_id = table.Column<int>(type: "integer", nullable: false, comment: "The method used for submitting payment by the consignee"),
                is_paid = table.Column<bool>(type: "boolean", nullable: false, comment: "Indicates if the invoice has been fully paid for"),
                date_invoiced = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "The date the customer was sent the invoice"),
                date_paid = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: null, comment: "The latest date the payment was submitted"),
                date_closed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: null, comment: "The final date when the invoice was fully processed and all the payment has cleared")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_invoices", x => x.id);
                table.ForeignKey(
                    name: "invoice_consignee_id_foreign",
                    column: x => x.consignee_id,
                    principalSchema: _schemaName,
                    principalTable: "clients",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "invoice_job_id_foreign",
                    column: x => x.job_id,
                    principalSchema: _schemaName,
                    principalTable: "jobs",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "invoice_payment_method_id_foreign",
                    column: x => x.payment_method_id,
                    principalSchema: _schemaName,
                    principalTable: "payment_methods",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.AddGuidTriggerFunction(_schemaName, _invoicesTableName);

        migrationBuilder.CreateTable(
            name: _invoiceLinesTableName,
            schema: _schemaName,
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                invoice_id = table.Column<int>(type: "integer", nullable: false),
                num_of_units = table.Column<int>(type: "integer", nullable: false, comment: "The number of the same line items in the invoice"),
                item_id = table.Column<int>(type: "integer", nullable: false, comment: "The item that is on the invoice"),
                price = table.Column<decimal>(type: "numeric", nullable: false, comment: "The total price of the item from the quantity")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InvoicesLines", x => x.id);
                table.ForeignKey(
                    name: "line_invoice_id_foreign",
                    column: x => x.invoice_id,
                    principalSchema: _schemaName,
                    principalTable: _invoicesTableName,
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "line_item_id_foreign",
                    column: x => x.item_id,
                    principalSchema: _schemaName,
                    principalTable: _lineItemsTableName,
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_invoices_consignee_id",
            schema: _schemaName,
            table: _invoicesTableName,
            column: "consignee_id");

        migrationBuilder.CreateIndex(
            name: "IX_invoices_job_id",
            schema: _schemaName,
            table: _invoicesTableName,
            column: "job_id");

        migrationBuilder.CreateIndex(
            name: "IX_invoices_payment_method_id",
            schema: _schemaName,
            table: _invoicesTableName,
            column: "payment_method_id");

        migrationBuilder.CreateIndex(
            name: "IX_InvoicesLines_invoice_id",
            schema: _schemaName,
            table: _invoiceLinesTableName,
            column: "invoice_id");

        migrationBuilder.CreateIndex(
            name: "IX_InvoicesLines_item_id",
            schema: _schemaName,
            table: _invoiceLinesTableName,
            column: "item_id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: _invoiceLinesTableName,
            schema: _schemaName);

        migrationBuilder.DropTable(
            name: _invoicesTableName,
            schema: _schemaName);

        migrationBuilder.DropTable(
            name: _lineItemsTableName,
            schema: _schemaName);

        migrationBuilder.DropTable(
            name: _paymentMethodsTableName,
            schema: _schemaName);

        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
            .OldAnnotation("Npgsql:Enum:crystal_clean.price_type", "none,per_unit,flat_rate")
            .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

        migrationBuilder.AlterColumn<DateTime>(
            name: "started_date",
            schema: _schemaName,
            table: "jobs",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            comment: "The date time the job was started",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldComment: "The date time the job was started");

        migrationBuilder.AlterColumn<DateTime>(
            name: "completed_date",
            schema: _schemaName,
            table: "jobs",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            comment: "The date time the job was completed",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldComment: "The date time the job was completed");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CanceledDate",
            schema: _schemaName,
            table: "jobs",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);
    }
}
