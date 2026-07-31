using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModerationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "moderation_schema");

            migrationBuilder.RenameTable(
                name: "user_reports",
                newName: "user_reports",
                newSchema: "moderation_schema");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "user_reports",
                schema: "moderation_schema",
                newName: "user_reports");
        }
    }
}
