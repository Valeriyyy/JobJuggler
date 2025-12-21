using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMetaLineItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meta_line_items",
                schema: "job_juggler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    invoice_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    subscription_id = table.Column<int>(type: "integer", nullable: true),
                    override_price = table.Column<decimal>(type: "numeric", nullable: true),
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
                    table.PrimaryKey("PK_meta_line_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_meta_line_items_users_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_meta_line_items_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_meta_line_items_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "meta_line_items_invoice_id_foreign",
                        column: x => x.invoice_id,
                        principalSchema: "job_juggler",
                        principalTable: "meta_invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "meta_line_items_product_id_foreign",
                        column: x => x.product_id,
                        principalSchema: "job_juggler",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "meta_line_items_subscription_id_foreign",
                        column: x => x.subscription_id,
                        principalSchema: "job_juggler",
                        principalTable: "subscriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meta_line_items_created_by_id",
                schema: "job_juggler",
                table: "meta_line_items",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_meta_line_items_deleted_by_id",
                schema: "job_juggler",
                table: "meta_line_items",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_meta_line_items_invoice_id",
                schema: "job_juggler",
                table: "meta_line_items",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_meta_line_items_last_modified_by_id",
                schema: "job_juggler",
                table: "meta_line_items",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_meta_line_items_product_id",
                schema: "job_juggler",
                table: "meta_line_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_meta_line_items_subscription_id",
                schema: "job_juggler",
                table: "meta_line_items",
                column: "subscription_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meta_line_items",
                schema: "job_juggler");
        }
    }
}
