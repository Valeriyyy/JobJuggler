using System;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Domain.MetaModels.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "job_juggler");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .Annotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription");

            migrationBuilder.CreateTable(
                name: "products",
                schema: "job_juggler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    type = table.Column<ProductType>(type: "job_juggler.product_type", nullable: false),
                    billing_period = table.Column<BillingPeriod>(type: "job_juggler.billing_period", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    product_options = table.Column<ProductOptions>(type: "jsonb", nullable: false),
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
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_users_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_products_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products",
                schema: "job_juggler");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .OldAnnotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription");
            
            migrationBuilder.DropSchema("job_juggler");
        }
    }
}
