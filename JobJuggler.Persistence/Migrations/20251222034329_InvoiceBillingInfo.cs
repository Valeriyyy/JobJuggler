using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceBillingInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "billing_info_id",
                schema: "job_juggler",
                table: "meta_invoices",
                type: "integer",
                nullable: true,
                comment: "The id of the billing information that was used for this invoice.");

            migrationBuilder.AddForeignKey(
                name: "FK_meta_invoices_companies_company_id",
                schema: "job_juggler",
                table: "meta_invoices",
                column: "company_id",
                principalSchema: "identity",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_meta_invoices_company_billing_info_billing_info_id",
                schema: "job_juggler",
                table: "meta_invoices",
                column: "billing_info_id",
                principalSchema: "job_juggler",
                principalTable: "company_billing_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meta_invoices_companies_company_id",
                schema: "job_juggler",
                table: "meta_invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_meta_invoices_company_billing_info_billing_info_id",
                schema: "job_juggler",
                table: "meta_invoices");

            migrationBuilder.DropColumn(
                name: "billing_info_id",
                schema: "job_juggler",
                table: "meta_invoices");
        }
    }
}
