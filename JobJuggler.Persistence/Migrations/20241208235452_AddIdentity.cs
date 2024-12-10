using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace JobJuggler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "invoice_payment_method_id_foreign",
                schema: "main",
                table: "invoices");

            // migrationBuilder.DropTable(
            //     name: "pg_type",
            //     schema: "main");

            migrationBuilder.DropIndex(
                name: "IX_invoices_payment_method_id",
                schema: "main",
                table: "invoices");
            
            // Create the identity schema
            migrationBuilder.Sql(File.ReadAllText(@"../JobJuggler.Persistence/Migrations/SQL/create_identity_schema.sql"));

            migrationBuilder.EnsureSchema(
                name: "identity");

            // migrationBuilder.AlterDatabase()
            //     .Annotation("Npgsql:Enum:main.price_type", "none,per_unit,flat_rate")
            //     .Annotation("Npgsql:Enum:price_type", "flat_rate,none,per_unit")
            //     .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
            //     .OldAnnotation("Npgsql:Enum:main.price_type", "none,per_unit,flat_rate")
            //     .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AlterColumn<int>(
                name: "payment_method_id",
                schema: "main",
                table: "invoices",
                type: "integer",
                nullable: true,
                comment: "The method used for submitting payment by the consignee",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "The method used for submitting payment by the consignee");


            migrationBuilder.AddColumn<DateTime>(
                name: "date_created",
                schema: "main",
                table: "clients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            
            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                schema: "main",
                table: "clients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_last_modified",
                schema: "main",
                table: "clients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "last_modified_by_id",
                schema: "main",
                table: "clients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "main",
                table: "clients",
                type: "boolean",
                nullable: false,
                defaultValue: false);
            
            migrationBuilder.AddColumn<DateTime>(
                name: "date_deleted",
                schema: "main",
                table: "clients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "deleted_by_id",
                schema: "main",
                table: "clients",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<NpgsqlTsVector>(
                name: "vector_address",
                schema: "main",
                table: "locations",
                type: "tsvector",
                nullable: false,
                computedColumnSql: "\n                        to_tsvector('english'::regconfig, \n                        ((((CASE WHEN (street1 IS NOT NULL) THEN ((street1)::text || ' '::text) ELSE ''::text END ||\n                            CASE WHEN (city IS NOT NULL) THEN ((city)::text || ' '::text) ELSE ''::text END) || \n                            CASE WHEN (state IS NOT NULL) THEN ((state)::text || ' '::text) ELSE ''::text END) || \n                            CASE WHEN (postal_code IS NOT NULL) THEN ((postal_code)::text || ' '::text) ELSE ''::text END) || \n                            (CASE WHEN (country IS NOT NULL) THEN country ELSE ''::character varying\r\nEND)::text))",
                stored: true,
                oldClrType: typeof(NpgsqlTsVector),
                oldType: "tsvector",
                oldComputedColumnSql: "\r\n                        to_tsvector('english'::regconfig, \r\n                        ((((CASE WHEN (street1 IS NOT NULL) THEN ((street1)::text || ' '::text) ELSE ''::text END ||\r\n                            CASE WHEN (city IS NOT NULL) THEN ((city)::text || ' '::text) ELSE ''::text END) || \r\n                            CASE WHEN (state IS NOT NULL) THEN ((state)::text || ' '::text) ELSE ''::text END) || \r\n                            CASE WHEN (postal_code IS NOT NULL) THEN ((postal_code)::text || ' '::text) ELSE ''::text END) || \r\n                            (CASE WHEN (country IS NOT NULL) THEN country ELSE ''::character varying\\nEND)::text))",
                oldStored: true);

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    display_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: false),
                    date_last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by_id = table.Column<int>(type: "integer", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<int>(type: "integer", nullable: true),
                    username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "user_company_id_foreign",
                        column: x => x.company_id,
                        principalSchema: "identity",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_claims_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "identity",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: false),
                    claim_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                schema: "identity",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "FK_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "identity",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "identity",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                schema: "identity",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "FK_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clients_created_by_id",
                schema: "main",
                table: "clients",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_clients_deleted_by_id",
                schema: "main",
                table: "clients",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_clients_last_modified_by_id",
                schema: "main",
                table: "clients",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_claims_role_id",
                schema: "identity",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "identity",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_user_id",
                schema: "identity",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_logins_user_id",
                schema: "identity",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                schema: "identity",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "identity",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "IX_users_company_id",
                schema: "identity",
                table: "users",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "identity",
                table: "users",
                column: "normalized_username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_users_created_by_id",
                schema: "main",
                table: "clients",
                column: "created_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_users_deleted_by_id",
                schema: "main",
                table: "clients",
                column: "deleted_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_clients_users_last_modified_by_id",
                schema: "main",
                table: "clients",
                column: "last_modified_by_id",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "invoice_payment_method_id_foreign",
                schema: "main",
                table: "invoices",
                column: "id",
                principalSchema: "main",
                principalTable: "payment_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_users_created_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_clients_users_deleted_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_clients_users_last_modified_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "invoice_payment_method_id_foreign",
                schema: "main",
                table: "invoices");

            migrationBuilder.DropTable(
                name: "role_claims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_claims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_logins",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_tokens",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "users",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "identity");

            migrationBuilder.DropIndex(
                name: "IX_clients_created_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_deleted_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_last_modified_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "date_created",
                schema: "main",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "date_deleted",
                schema: "main",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "date_last_modified",
                schema: "main",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "deleted_by_id",
                schema: "main",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "main",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "last_modified_by_id",
                schema: "main",
                table: "clients");

            // migrationBuilder.AlterDatabase()
            //     .Annotation("Npgsql:Enum:main.price_type", "none,per_unit,flat_rate")
            //     .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
            //     .OldAnnotation("Npgsql:Enum:main.price_type", "none,per_unit,flat_rate")
            //     .OldAnnotation("Npgsql:Enum:price_type", "flat_rate,none,per_unit")
            //     .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AlterColumn<int>(
                name: "payment_method_id",
                schema: "main",
                table: "invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "The method used for submitting payment by the consignee",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "The method used for submitting payment by the consignee");

            migrationBuilder.AlterColumn<NpgsqlTsVector>(
                name: "vector_address",
                schema: "main",
                table: "locations",
                type: "tsvector",
                nullable: false,
                computedColumnSql: "\r\n                        to_tsvector('english'::regconfig, \r\n                        ((((CASE WHEN (street1 IS NOT NULL) THEN ((street1)::text || ' '::text) ELSE ''::text END ||\r\n                            CASE WHEN (city IS NOT NULL) THEN ((city)::text || ' '::text) ELSE ''::text END) || \r\n                            CASE WHEN (state IS NOT NULL) THEN ((state)::text || ' '::text) ELSE ''::text END) || \r\n                            CASE WHEN (postal_code IS NOT NULL) THEN ((postal_code)::text || ' '::text) ELSE ''::text END) || \r\n                            (CASE WHEN (country IS NOT NULL) THEN country ELSE ''::character varying\r\nEND)::text))",
                stored: true,
                oldClrType: typeof(NpgsqlTsVector),
                oldType: "tsvector",
                oldComputedColumnSql: "\n                        to_tsvector('english'::regconfig, \n                        ((((CASE WHEN (street1 IS NOT NULL) THEN ((street1)::text || ' '::text) ELSE ''::text END ||\n                            CASE WHEN (city IS NOT NULL) THEN ((city)::text || ' '::text) ELSE ''::text END) || \n                            CASE WHEN (state IS NOT NULL) THEN ((state)::text || ' '::text) ELSE ''::text END) || \n                            CASE WHEN (postal_code IS NOT NULL) THEN ((postal_code)::text || ' '::text) ELSE ''::text END) || \n                            (CASE WHEN (country IS NOT NULL) THEN country ELSE ''::character varying\nEND)::text))",
                oldStored: true);

            // migrationBuilder.CreateTable(
            //     name: "pg_type",
            //     schema: "main",
            //     columns: table => new
            //     {
            //         enum_name = table.Column<string>(type: "text", nullable: false),
            //         enum_value = table.Column<string>(type: "text", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //     });

            migrationBuilder.CreateIndex(
                name: "IX_invoices_payment_method_id",
                schema: "main",
                table: "invoices",
                column: "payment_method_id");

            migrationBuilder.AddForeignKey(
                name: "invoice_payment_method_id_foreign",
                schema: "main",
                table: "invoices",
                column: "payment_method_id",
                principalSchema: "main",
                principalTable: "payment_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
