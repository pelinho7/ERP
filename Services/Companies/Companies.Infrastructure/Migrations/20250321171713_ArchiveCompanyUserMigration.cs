using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ArchiveCompanyUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "CompanyUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "CompanyUsers");
        }
    }
}
