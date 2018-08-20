using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class ProductCourier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourierGuid",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourierUserGuid",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CourierUserGuid",
                table: "Products",
                column: "CourierUserGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CourierUserGuid",
                table: "Products",
                column: "CourierUserGuid",
                principalTable: "Users",
                principalColumn: "UserGuid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CourierUserGuid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CourierUserGuid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CourierGuid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CourierUserGuid",
                table: "Products");
        }
    }
}
