using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlinePerfumes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SchoolMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderStatusId2",
                table: "OrderStatusUpdate");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatusUpdate_OrderStatusId2",
                table: "OrderStatusUpdate");

            migrationBuilder.DropColumn(
                name: "OrderStatusId2",
                table: "OrderStatusUpdate");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId2",
                table: "OrderStatusUpdate",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusUpdate_OrderStatusId2",
                table: "OrderStatusUpdate",
                column: "OrderStatusId2");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusUpdate_Orders_OrderStatusId2",
                table: "OrderStatusUpdate",
                column: "OrderStatusId2",
                principalTable: "Orders",
                principalColumn: "OrderStatusId");
        }
    }
}
