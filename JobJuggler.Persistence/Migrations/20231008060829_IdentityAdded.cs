using System;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations;

/// <inheritdoc />
public partial class IdentityAdded : Migration {
    const string _identitySchemaName = "identity";
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        // Create the identity schema
        migrationBuilder.Sql(File.ReadAllText(@"..\JobJuggler.Persistence\Migrations\SQL\create_identity_schema.sql"));

        // Ensure that it has been created and is ready for tables
        migrationBuilder.EnsureSchema(
            name: _identitySchemaName);


        migrationBuilder.CreateTable(
            name: "asp_net_roles",
            schema: _identitySchemaName,
            columns: table => new {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalizedname = table.Column<string>(name: "normalized_name", type: "character varying(256)", maxLength: 256, nullable: true),
                concurrencystamp = table.Column<string>(name: "concurrency_stamp", type: "text", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_asp_net_roles", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "companies",
            schema: _identitySchemaName,
            columns: table => new {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false),
                createdbyid = table.Column<int>(name: "created_by_id", type: "integer", nullable: false),
                datelastmodified = table.Column<DateTime>(name: "date_last_modified", type: "timestamp with time zone", nullable: false),
                lastmodifiedbyid = table.Column<int>(name: "last_modified_by_id", type: "integer", nullable: false),
                isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false),
                datedeleted = table.Column<DateTime>(name: "date_deleted", type: "timestamp with time zone", nullable: false),
                deletedbyid = table.Column<int>(name: "deleted_by_id", type: "integer", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_companies", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "asp_net_role_claims",
            schema: _identitySchemaName,
            columns: table => new {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false),
                claimtype = table.Column<string>(name: "claim_type", type: "text", nullable: true),
                claimvalue = table.Column<string>(name: "claim_value", type: "text", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_asp_net_role_claims", x => x.id);
                table.ForeignKey(
                    name: "FK_asp_net_role_claims_asp_net_roles_role_id",
                    column: x => x.roleid,
                    principalSchema: _identitySchemaName,
                    principalTable: "asp_net_roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "asp_net_users",
            schema: _identitySchemaName,
            columns: table => new {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                displayname = table.Column<string>(name: "display_name", type: "text", nullable: true),
                companyid = table.Column<int>(name: "company_id", type: "integer", nullable: false),
                datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false),
                createdbyid = table.Column<int>(name: "created_by_id", type: "integer", nullable: false),
                datelastmodified = table.Column<DateTime>(name: "date_last_modified", type: "timestamp with time zone", nullable: false),
                lastmodifiedbyid = table.Column<int>(name: "last_modified_by_id", type: "integer", nullable: false),
                isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                datedeleted = table.Column<DateTime>(name: "date_deleted", type: "timestamp with time zone", nullable: false),
                deletedbyid = table.Column<int>(name: "deleted_by_id", type: "integer", nullable: false),
                username = table.Column<string>(name: "username", type: "character varying(256)", maxLength: 256, nullable: true),
                normalizedusername = table.Column<string>(name: "normalized_username", type: "character varying(256)", maxLength: 256, nullable: true),
                email = table.Column<string>(name: "email", type: "character varying(256)", maxLength: 256, nullable: true),
                normalizedemail = table.Column<string>(name: "normalized_email", type: "character varying(256)", maxLength: 256, nullable: true),
                emailconfirmed = table.Column<bool>(name: "email_confirmed", type: "boolean", nullable: false),
                passwordhash = table.Column<string>(name: "password_hash", type: "text", nullable: true),
                securitystamp = table.Column<string>(name: "security_stamp", type: "text", nullable: true),
                concurrencystamp = table.Column<string>(name: "concurrency_stamp", type: "text", nullable: true),
                phonenumber = table.Column<string>(name: "phone_number", type: "text", nullable: true),
                phonenumberconfirmed = table.Column<bool>(name: "phone_number_confirmed", type: "boolean", nullable: false),
                twofactorenabled = table.Column<bool>(name: "two_factor_enabled", type: "boolean", nullable: false),
                lockoutend = table.Column<DateTimeOffset>(name: "lockout_end", type: "timestamp with time zone", nullable: true),
                lockoutenabled = table.Column<bool>(name: "lockout_enabled", type: "boolean", nullable: false),
                accessfailedcount = table.Column<int>(name: "access_failed_count", type: "integer", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_asp_net_users", x => x.id);
                table.ForeignKey(
                    name: "FK_asp_net_users_companies_company_id",
                    column: x => x.companyid,
                    principalSchema: _identitySchemaName,
                    principalTable: "companies",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "asp_net_user_claims",
            schema: _identitySchemaName,
            columns: table => new {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                claimtype = table.Column<string>(name: "claim_type", type: "text", nullable: true),
                claimvalue = table.Column<string>(name: "claim_value", type: "text", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_asp_net_user_claims", x => x.id);
                table.ForeignKey(
                    name: "FK_asp_net_user_claims_asp_net_users_user_id",
                    column: x => x.userid,
                    principalSchema: _identitySchemaName,
                    principalTable: "asp_net_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "asp_net_user_logins",
            schema: _identitySchemaName,
            columns: table => new {
                loginprovider = table.Column<string>(name: "login_provider", type: "text", nullable: false),
                providerkey = table.Column<string>(name: "provider_key", type: "text", nullable: false),
                providerdisplayname = table.Column<string>(name: "provider_display_name", type: "text", nullable: true),
                userid = table.Column<int>(name: "user_id", type: "integer", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_asp_net_user_logins", x => new { x.loginprovider, x.providerkey });
                table.ForeignKey(
                    name: "FK_asp_net_user_logins_asp_net_users_user_id",
                    column: x => x.userid,
                    principalSchema: _identitySchemaName,
                    principalTable: "asp_net_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "asp_net_user_roles",
            schema: _identitySchemaName,
            columns: table => new {
                userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_asp_net_user_roles", x => new { x.userid, x.roleid });
                table.ForeignKey(
                    name: "FK_asp_net_user_roles_asp_net_roles_role_id",
                    column: x => x.roleid,
                    principalSchema: _identitySchemaName,
                    principalTable: "asp_net_roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_asp_net_user_roles_asp_net_users_user_id",
                    column: x => x.userid,
                    principalSchema: _identitySchemaName,
                    principalTable: "asp_net_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "asp_net_user_tokens",
            schema: _identitySchemaName,
            columns: table => new {
                userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                loginprovider = table.Column<string>(name: "login_provider", type: "text", nullable: false),
                name = table.Column<string>(name: "name", type: "text", nullable: false),
                value = table.Column<string>(name: "value", type: "text", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_asp_net_user_tokens", x => new { x.userid, x.loginprovider, x.name });
                table.ForeignKey(
                    name: "FK_asp_net_user_tokens_asp_net_users_user_id",
                    column: x => x.userid,
                    principalSchema: _identitySchemaName,
                    principalTable: "asp_net_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_asp_net_role_claims_role_id",
            schema: _identitySchemaName,
            table: "asp_net_role_claims",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            schema: _identitySchemaName,
            table: "asp_net_roles",
            column: "normalized_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_asp_net_user_claims_user_id",
            schema: _identitySchemaName,
            table: "asp_net_user_claims",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "IX_asp_net_user_logins_user_id",
            schema: _identitySchemaName,
            table: "asp_net_user_logins",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "IX_asp_net_user_roles_role_id",
            schema: _identitySchemaName,
            table: "asp_net_user_roles",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            schema: _identitySchemaName,
            table: "asp_net_users",
            column: "normalized_email");

        migrationBuilder.CreateIndex(
            name: "IX_asp_net_users_company_id",
            schema: _identitySchemaName,
            table: "asp_net_users",
            column: "company_id");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            schema: _identitySchemaName,
            table: "asp_net_users",
            column: "normalized_username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "asp_net_role_claims",
            schema: _identitySchemaName);

        migrationBuilder.DropTable(
            name: "asp_net_user_claims",
            schema: _identitySchemaName);

        migrationBuilder.DropTable(
            name: "asp_net_user_logins",
            schema: _identitySchemaName);

        migrationBuilder.DropTable(
            name: "asp_net_user_roles",
            schema: _identitySchemaName);

        migrationBuilder.DropTable(
            name: "asp_net_user_tokens",
            schema: _identitySchemaName);

        migrationBuilder.DropTable(
            name: "asp_net_roles",
            schema: _identitySchemaName);

        migrationBuilder.DropTable(
            name: "asp_net_users",
            schema: _identitySchemaName);

        migrationBuilder.DropTable(
            name: "companies",
            schema: _identitySchemaName);

        migrationBuilder.Sql("DROP SCHEMA IF EXISTS identity");
    }
}
