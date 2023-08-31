using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PKWiU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VatValue = table.Column<decimal>(type: "decimal(2,2)", nullable: false),
                    VatFlag = table.Column<int>(type: "int", nullable: false),
                    SplitPayment = table.Column<bool>(type: "bit", nullable: false),
                    SaleCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaleNetPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SaleGrossPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PurchaseNetPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PurchaseGrossPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PurchaseCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockLevelControl = table.Column<bool>(type: "bit", nullable: false),
                    StockLevel = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
