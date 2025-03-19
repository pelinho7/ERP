using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "varchar(300)", nullable: false),
                    CountryCode = table.Column<string>(type: "varchar(2)", nullable: false),
                    ActiveTaxpayer = table.Column<bool>(type: "bit", nullable: false),
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
                    PaymentForm = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "varchar(34)", nullable: false),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
