using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyBillingInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_company_id",
                schema: "identity",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_user_roles_role_id",
                schema: "identity",
                table: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_logins_user_id",
                schema: "identity",
                table: "user_logins");

            migrationBuilder.DropIndex(
                name: "IX_user_claims_user_id",
                schema: "identity",
                table: "user_claims");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_company_id",
                schema: "job_juggler",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_created_by_id",
                schema: "job_juggler",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_deleted_by_id",
                schema: "job_juggler",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_last_modified_by_id",
                schema: "job_juggler",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_product_id",
                schema: "job_juggler",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_role_claims_role_id",
                schema: "identity",
                table: "role_claims");

            migrationBuilder.DropIndex(
                name: "IX_products_created_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_deleted_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_last_modified_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_meta_line_items_created_by_id",
                schema: "job_juggler",
                table: "meta_line_items");

            migrationBuilder.DropIndex(
                name: "IX_meta_line_items_deleted_by_id",
                schema: "job_juggler",
                table: "meta_line_items");

            migrationBuilder.DropIndex(
                name: "IX_meta_line_items_invoice_id",
                schema: "job_juggler",
                table: "meta_line_items");

            migrationBuilder.DropIndex(
                name: "IX_meta_line_items_last_modified_by_id",
                schema: "job_juggler",
                table: "meta_line_items");

            migrationBuilder.DropIndex(
                name: "IX_meta_line_items_product_id",
                schema: "job_juggler",
                table: "meta_line_items");

            migrationBuilder.DropIndex(
                name: "IX_meta_line_items_subscription_id",
                schema: "job_juggler",
                table: "meta_line_items");

            migrationBuilder.DropIndex(
                name: "IX_meta_invoices_created_by_id",
                schema: "job_juggler",
                table: "meta_invoices");

            migrationBuilder.DropIndex(
                name: "IX_meta_invoices_deleted_by_id",
                schema: "job_juggler",
                table: "meta_invoices");

            migrationBuilder.DropIndex(
                name: "IX_meta_invoices_last_modified_by_id",
                schema: "job_juggler",
                table: "meta_invoices");

            migrationBuilder.DropIndex(
                name: "IX_contacts_company_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropIndex(
                name: "IX_contacts_created_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropIndex(
                name: "IX_contacts_deleted_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropIndex(
                name: "IX_contacts_last_modified_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropIndex(
                name: "IX_companies_created_by_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropIndex(
                name: "IX_companies_deleted_by_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropIndex(
                name: "IX_companies_last_modified_by_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropIndex(
                name: "IX_companies_primary_contact_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.CreateTable(
                name: "company_billing_info",
                schema: "job_juggler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    contact_id = table.Column<int>(type: "integer", nullable: false),
                    payment_token = table.Column<string>(type: "text", nullable: false),
                    last_4_digits = table.Column<string>(type: "text", nullable: false),
                    card_brand = table.Column<string>(type: "text", nullable: false),
                    exp_month = table.Column<string>(type: "text", nullable: false),
                    exp_year = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_company_billing_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_billing_info_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "identity",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_company_billing_info_contacts_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "job_juggler",
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_company_billing_info_users_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_company_billing_info_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_company_billing_info_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_billing_info",
                schema: "job_juggler");

            migrationBuilder.CreateIndex(
                name: "IX_users_company_id",
                schema: "identity",
                table: "users",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                schema: "identity",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_logins_user_id",
                schema: "identity",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_user_id",
                schema: "identity",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_company_id",
                schema: "job_juggler",
                table: "subscriptions",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_created_by_id",
                schema: "job_juggler",
                table: "subscriptions",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_deleted_by_id",
                schema: "job_juggler",
                table: "subscriptions",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_last_modified_by_id",
                schema: "job_juggler",
                table: "subscriptions",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_product_id",
                schema: "job_juggler",
                table: "subscriptions",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_claims_role_id",
                schema: "identity",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_created_by_id",
                schema: "job_juggler",
                table: "products",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_deleted_by_id",
                schema: "job_juggler",
                table: "products",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_last_modified_by_id",
                schema: "job_juggler",
                table: "products",
                column: "last_modified_by_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_contacts_company_id",
                schema: "job_juggler",
                table: "contacts",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_contacts_created_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_contacts_deleted_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_contacts_last_modified_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_created_by_id",
                schema: "identity",
                table: "companies",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_deleted_by_id",
                schema: "identity",
                table: "companies",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_last_modified_by_id",
                schema: "identity",
                table: "companies",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_primary_contact_id",
                schema: "identity",
                table: "companies",
                column: "primary_contact_id");
        }
    }
}
