using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class UserManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");
        }
    }
}
