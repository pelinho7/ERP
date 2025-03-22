using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HistoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedModifiedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(300)", nullable: false),
                    CountryCode = table.Column<string>(type: "varchar(2)", nullable: false),
                    VatId = table.Column<string>(type: "varchar(13)", nullable: false),
                    Street = table.Column<string>(type: "varchar(150)", nullable: false),
                    StreetNo = table.Column<string>(type: "varchar(10)", nullable: false),
                    ApartmentNo = table.Column<string>(type: "varchar(10)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(6)", nullable: false),
                    PostalOffice = table.Column<string>(type: "varchar(120)", nullable: false),
                    Country = table.Column<string>(type: "varchar(80)", nullable: false),
                    City = table.Column<string>(type: "varchar(120)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(30)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(30)", nullable: false),
                    Fax = table.Column<string>(type: "varchar(30)", nullable: false),
                    WwwAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "varchar(34)", nullable: false),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHistory_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedModifiedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Administrator = table.Column<bool>(type: "bit", nullable: false),
                    ContractorsModuleRead = table.Column<bool>(type: "bit", nullable: false),
                    ContractorsModuleWrite = table.Column<bool>(type: "bit", nullable: false),
                    ProductsModuleRead = table.Column<bool>(type: "bit", nullable: false),
                    ProductsModuleWrite = table.Column<bool>(type: "bit", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUserHistory_CompanyUsers_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistory_CompanyId",
                table: "CompanyHistory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserHistory_CompanyUserId",
                table: "CompanyUserHistory",
                column: "CompanyUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyHistory");

            migrationBuilder.DropTable(
                name: "CompanyUserHistory");
        }
    }
}
