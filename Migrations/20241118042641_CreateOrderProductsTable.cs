using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace onboarding_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order_Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Products_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Products_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Products_Order_Id",
                table: "Order_Products",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Products_Product_Id",
                table: "Order_Products",
                column: "Product_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Products");
        }
    }
}
