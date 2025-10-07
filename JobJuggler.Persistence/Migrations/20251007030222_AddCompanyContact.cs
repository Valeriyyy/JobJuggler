using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contacts_users_created_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_contacts_users_deleted_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_contacts_users_last_modified_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_products_users_created_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_users_deleted_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_users_last_modified_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .Annotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription")
                .Annotation("Npgsql:Enum:job_juggler.subscription_status", "active,canceled,inactive,past_due,terminated")
                .OldAnnotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .OldAnnotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription");

            migrationBuilder.CreateIndex(
                name: "IX_contacts_company_id",
                schema: "job_juggler",
                table: "contacts",
                column: "company_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_companies_users_created_by_id",
                schema: "identity",
                table: "companies",
                column: "created_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_companies_users_deleted_by_id",
                schema: "identity",
                table: "companies",
                column: "deleted_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_companies_users_last_modified_by_id",
                schema: "identity",
                table: "companies",
                column: "last_modified_by_id",
                principalSchema: "identity",
                principalTable: "users",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_users_created_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "created_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_users_deleted_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "deleted_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_users_last_modified_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "last_modified_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_created_by_id",
                schema: "job_juggler",
                table: "products",
                column: "created_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_deleted_by_id",
                schema: "job_juggler",
                table: "products",
                column: "deleted_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_last_modified_by_id",
                schema: "job_juggler",
                table: "products",
                column: "last_modified_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_users_created_by_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_companies_users_deleted_by_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_companies_users_last_modified_by_id",
                schema: "identity",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_contacts_companies_company_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_contacts_users_created_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_contacts_users_deleted_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_contacts_users_last_modified_by_id",
                schema: "job_juggler",
                table: "contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_products_users_created_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_users_deleted_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_users_last_modified_by_id",
                schema: "job_juggler",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_contacts_company_id",
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

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .Annotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription")
                .OldAnnotation("Npgsql:Enum:job_juggler.billing_period", "monthly,one_time,yearly")
                .OldAnnotation("Npgsql:Enum:job_juggler.product_type", "legacy,metered,one_time,subscription")
                .OldAnnotation("Npgsql:Enum:job_juggler.subscription_status", "active,canceled,inactive,past_due,terminated");

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_users_created_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "created_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_users_deleted_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "deleted_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_users_last_modified_by_id",
                schema: "job_juggler",
                table: "contacts",
                column: "last_modified_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_created_by_id",
                schema: "job_juggler",
                table: "products",
                column: "created_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_deleted_by_id",
                schema: "job_juggler",
                table: "products",
                column: "deleted_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_last_modified_by_id",
                schema: "job_juggler",
                table: "products",
                column: "last_modified_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
