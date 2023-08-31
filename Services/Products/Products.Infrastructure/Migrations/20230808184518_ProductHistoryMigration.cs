using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductHistoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedModifiedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PKWiU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VatValue = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    VatFlag = table.Column<int>(type: "int", nullable: false),
                    SplitPayment = table.Column<bool>(type: "bit", nullable: false),
                    SaleCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaleNetPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SaleGrossPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PurchaseNetPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PurchaseGrossPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PurchaseCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockLevelControl = table.Column<bool>(type: "bit", nullable: false),
                    StockLevel = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_ProductId",
                table: "ProductHistories",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductHistories");
        }
    }
}
