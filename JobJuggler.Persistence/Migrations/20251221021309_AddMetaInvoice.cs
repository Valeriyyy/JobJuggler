using System;
using JobJuggler.Domain.MetaModels;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMetaInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .Annotation("Npgsql:Enum:job_juggler.invoice_status", "canceled,failed,none,paid,pending")
                .Annotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription")
                .Annotation("Npgsql:Enum:job_juggler.subscription_status", "active,canceled,inactive,past_due,terminated")
                .OldAnnotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .OldAnnotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription")
                .OldAnnotation("Npgsql:Enum:job_juggler.subscription_status", "active,canceled,inactive,past_due,terminated");

            migrationBuilder.CreateTable(
                name: "meta_invoices",
                schema: "job_juggler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<InvoiceStatus>(type: "job_juggler.invoice_status", nullable: false, defaultValue: InvoiceStatus.None),
                    sub_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0.00m),
                    tax = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0.00m),
                    total = table.Column<decimal>(type: "numeric(18,2)", nullable: false, computedColumnSql: "\"sub_total\" + \"tax\"", stored: true),
                    date_paid = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'UTC'"),
                    created_by_id = table.Column<int>(type: "integer", nullable: false),
                    date_last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by_id = table.Column<int>(type: "integer", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meta_invoices", x => x.id);
                    table.ForeignKey(
                        name: "FK_meta_invoices_users_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_meta_invoices_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_meta_invoices_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meta_invoices_created_by_id",
                schema: "job_juggler",
                table: "meta_invoices",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_meta_invoices_deleted_by_id",
                schema: "job_juggler",
                table: "meta_invoices",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_meta_invoices_last_modified_by_id",
                schema: "job_juggler",
                table: "meta_invoices",
                column: "last_modified_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meta_invoices",
                schema: "job_juggler");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .Annotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription")
                .Annotation("Npgsql:Enum:job_juggler.subscription_status", "active,canceled,inactive,past_due,terminated")
                .OldAnnotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .OldAnnotation("Npgsql:Enum:job_juggler.invoice_status", "canceled,failed,none,paid,pending")
                .OldAnnotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription")
                .OldAnnotation("Npgsql:Enum:job_juggler.subscription_status", "active,canceled,inactive,past_due,terminated");
        }
    }
}
