using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlinePerfumes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class School2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderId",
                table: "OrderStatusUpdate");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId1",
                table: "OrderStatusUpdate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId2",
                table: "OrderStatusUpdate",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusUpdate_OrderStatusId1",
                table: "OrderStatusUpdate",
                column: "OrderStatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusUpdate_OrderStatusId2",
                table: "OrderStatusUpdate",
                column: "OrderStatusId2");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusUpdate_OrderStatus_OrderStatusId1",
                table: "OrderStatusUpdate",
                column: "OrderStatusId1",
                principalTable: "OrderStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderId",
                table: "OrderStatusUpdate",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderStatusId2",
                table: "OrderStatusUpdate",
                column: "OrderStatusId2",
                principalTable: "Orders",
                principalColumn: "OrderStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusUpdate_OrderStatus_OrderStatusId1",
                table: "OrderStatusUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderId",
                table: "OrderStatusUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderStatusId2",
                table: "OrderStatusUpdate");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatusUpdate_OrderStatusId1",
                table: "OrderStatusUpdate");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatusUpdate_OrderStatusId2",
                table: "OrderStatusUpdate");

            migrationBuilder.DropColumn(
                name: "OrderStatusId1",
                table: "OrderStatusUpdate");

            migrationBuilder.DropColumn(
                name: "OrderStatusId2",
                table: "OrderStatusUpdate");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderId",
                table: "OrderStatusUpdate",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
