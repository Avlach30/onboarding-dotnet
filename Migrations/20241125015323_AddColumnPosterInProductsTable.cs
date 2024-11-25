using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace onboarding_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnPosterInProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "poster",
                table: "products",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "poster",
                table: "products");
        }
    }
}
