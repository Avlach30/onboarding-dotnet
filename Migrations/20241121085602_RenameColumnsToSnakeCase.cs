using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace onboarding_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_products_orders_Order_Id",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "FK_order_products_products_Product_Id",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_User_Id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "users",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "users",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "transactions",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "transactions",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "transactions",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "products",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "products",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "products",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "products",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_category_id");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "orders",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "orders",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Total_Price",
                table: "orders",
                newName: "total_price");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "orders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "orders",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "orders",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_User_Id",
                table: "orders",
                newName: "IX_orders_user_id");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "order_products",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "order_products",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "order_products",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "Order_Id",
                table: "order_products",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "order_products",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "order_products",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "order_products",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_order_products_Product_Id",
                table: "order_products",
                newName: "IX_order_products_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_products_Order_Id",
                table: "order_products",
                newName: "IX_order_products_order_id");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "categories",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "categories",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "categories",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_orders_order_id",
                table: "order_products",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_products_product_id",
                table: "order_products",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_products_orders_order_id",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "FK_order_products_products_product_id",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_category_id",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "users",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "users",
                newName: "Deleted_at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "users",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "transactions",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "transactions",
                newName: "Deleted_at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "transactions",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "products",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "products",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "products",
                newName: "Deleted_at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "products",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_category_id",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "orders",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "orders",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "orders",
                newName: "Total_Price");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "orders",
                newName: "Deleted_at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "orders",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "orders",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_user_id",
                table: "orders",
                newName: "IX_orders_User_Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "order_products",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "order_products",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order_products",
                newName: "Product_Id");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "order_products",
                newName: "Order_Id");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "order_products",
                newName: "Deleted_at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "order_products",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "order_products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_order_products_product_id",
                table: "order_products",
                newName: "IX_order_products_Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_order_products_order_id",
                table: "order_products",
                newName: "IX_order_products_Order_Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "categories",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "categories",
                newName: "Deleted_at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "categories",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categories",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_orders_Order_Id",
                table: "order_products",
                column: "Order_Id",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_products_Product_Id",
                table: "order_products",
                column: "Product_Id",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_User_Id",
                table: "orders",
                column: "User_Id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
