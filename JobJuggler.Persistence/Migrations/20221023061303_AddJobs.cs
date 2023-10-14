using JobJuggler.Persistence.Migrations.Tools;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobJuggler.Persistence.Migrations;

public partial class AddJobs : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        var schemaName = "crystal_clean";
        var tableName = "jobs";
        migrationBuilder.CreateTable(
            name: tableName,
            schema: schemaName,
            columns: table => new {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                client_id = table.Column<int>(type: "integer", nullable: false, comment: "The main person or business requesting the service"),
                location_id = table.Column<int>(type: "integer", nullable: false),
                price = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false, comment: "The total sum price of all the line items related to the order"),
                notes = table.Column<string>(type: "text", nullable: true, comment: "General notes about the order"),
                is_completed = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Indicates if the job is fully complete meaning all payments have cleared"),
                is_canceled = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Indicates if the job has been canceled"),
                cancel_reason = table.Column<string>(type: "text", nullable: true, comment: "Brief explanation of why the job was canceled"),
                scheduled_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "The date the job was booked"),
                scheduled_arrival_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "The scheduled beginning datetime that the vendor will arrive at the job location"),
                scheduled_arrival_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "The scheduled end datetime that the vendor will arrive at the job location"),
                started_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: null, comment: "The date time the job was started"),
                completed_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: null, comment: "The date time the job was completed"),
                canceled_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: null, comment: "The date time the job was canceled")
            },
            constraints: table => {
                table.PrimaryKey("PK_jobs", x => x.id);
                table.ForeignKey(
                    name: "job_client_id_foreign",
                    column: x => x.client_id,
                    principalSchema: "crystal_clean",
                    principalTable: "clients",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "job_location_id_foreign",
                    column: x => x.location_id,
                    principalSchema: "crystal_clean",
                    principalTable: "locations",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_jobs_client_id",
            schema: "crystal_clean",
            table: "jobs",
            column: "client_id");

        migrationBuilder.CreateIndex(
            name: "IX_jobs_location_id",
            schema: "crystal_clean",
            table: "jobs",
            column: "location_id");

        migrationBuilder.AddGuidTriggerFunction(schemaName, tableName);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "jobs",
            schema: "crystal_clean");
    }
}
