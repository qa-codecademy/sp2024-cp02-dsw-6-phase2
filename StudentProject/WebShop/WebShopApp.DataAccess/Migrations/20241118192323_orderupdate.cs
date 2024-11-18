using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class orderupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderrId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderrId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderrId",
                table: "OrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderrId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderrId",
                table: "OrderItems",
                column: "OrderrId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderrId",
                table: "OrderItems",
                column: "OrderrId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
