using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class ProductCourierFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CourierUserGuid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CourierUserGuid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CourierUserGuid",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CourierGuid",
                table: "Products",
                column: "CourierGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CourierGuid",
                table: "Products",
                column: "CourierGuid",
                principalTable: "Users",
                principalColumn: "UserGuid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CourierGuid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CourierGuid",
                table: "Products");

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
    }
}
