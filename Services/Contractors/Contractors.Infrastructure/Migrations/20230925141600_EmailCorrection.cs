using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contractors.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmailCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "Contractors",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "ContractorHistories",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Contractors",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ContractorHistories",
                newName: "email");
        }
    }
}
