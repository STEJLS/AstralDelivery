using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class UserPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeliveryPointGuid",
                table: "Products",
                column: "DeliveryPointGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DeliveryPoints_DeliveryPointGuid",
                table: "Products",
                column: "DeliveryPointGuid",
                principalTable: "DeliveryPoints",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_DeliveryPoints_DeliveryPointGuid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeliveryPointGuid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "Products");
        }
    }
}
