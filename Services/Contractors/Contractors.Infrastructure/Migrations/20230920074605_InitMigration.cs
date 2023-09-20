using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contractors.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(30)", nullable: false),
                    Name = table.Column<string>(type: "varchar(300)", nullable: false),
                    RepFirstName = table.Column<string>(type: "varchar(100)", nullable: false),
                    RepLastName = table.Column<string>(type: "varchar(150)", nullable: false),
                    CountryCode = table.Column<string>(type: "varchar(2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(30)", nullable: false),
                    Fax = table.Column<string>(type: "varchar(30)", nullable: false),
                    WwwAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PaymentForm = table.Column<int>(type: "int", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "varchar(34)", nullable: false),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SplitPayment = table.Column<bool>(type: "bit", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedModifiedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(30)", nullable: false),
                    Name = table.Column<string>(type: "varchar(300)", nullable: false),
                    RepFirstName = table.Column<string>(type: "varchar(100)", nullable: false),
                    RepLastName = table.Column<string>(type: "varchar(150)", nullable: false),
                    CountryCode = table.Column<string>(type: "varchar(2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(30)", nullable: false),
                    Fax = table.Column<string>(type: "varchar(30)", nullable: false),
                    WwwAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PaymentForm = table.Column<int>(type: "int", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "varchar(34)", nullable: false),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SplitPayment = table.Column<bool>(type: "bit", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorHistories_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorHistories_ContractorId",
                table: "ContractorHistories",
                column: "ContractorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorHistories");

            migrationBuilder.DropTable(
                name: "Contractors");
        }
    }
}
