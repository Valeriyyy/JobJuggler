using System;
using JobJuggler.Domain.MetaModels.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contacts_companies_company_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.AddColumn<int>(
                name: "primary_contact_id",
                schema: "identity",
                table: "companies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "subscriptions",
                schema: "job_juggler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<SubscriptionStatus>(type: "job_juggler.subscription_status", nullable: false),
                    price_override = table.Column<decimal>(type: "numeric", nullable: true),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: false),
                    date_last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by_id = table.Column<int>(type: "integer", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_subscriptions_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "identity",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_subscriptions_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "job_juggler",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_subscriptions_users_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_subscriptions_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_subscriptions_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companies_primary_contact_id",
                schema: "identity",
                table: "companies",
                column: "primary_contact_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_companies_contacts_primary_contact_id",
                schema: "identity",
                table: "companies",
                column: "primary_contact_id",
                principalSchema: "job_juggler",
                principalTable: "contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_companies_company_id",
                schema: "job_juggler",
                table: "contacts",
                column: "company_id",
                principalSchema: "identity",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_contacts_primary_contact_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_contacts_companies_company_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropTable(
                name: "subscriptions",
                schema: "job_juggler");

            migrationBuilder.DropIndex(
                name: "IX_companies_primary_contact_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "primary_contact_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_companies_company_id",
                schema: "job_juggler",
                table: "contacts",
                column: "company_id",
                principalSchema: "identity",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
