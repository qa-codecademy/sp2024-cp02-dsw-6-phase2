using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class idk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProducttId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "ProducttId",
                table: "OrderItem",
                newName: "OrderrId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ProducttId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderrId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderrId",
                table: "OrderItem",
                column: "OrderrId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderrId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "OrderrId",
                table: "OrderItem",
                newName: "ProducttId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderrId",
                table: "OrderItem",
                newName: "IX_OrderItem_ProducttId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProducttId",
                table: "OrderItem",
                column: "ProducttId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
