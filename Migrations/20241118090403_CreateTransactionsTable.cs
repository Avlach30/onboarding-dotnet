using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace onboarding_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class CreateTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payment_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_order_id",
                table: "transactions",
                column: "order_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
