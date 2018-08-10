using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class RenamePasswordRecoveries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_passwordRecoveries_Users_UserGuid",
                table: "passwordRecoveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_passwordRecoveries",
                table: "passwordRecoveries");

            migrationBuilder.RenameTable(
                name: "passwordRecoveries",
                newName: "PasswordRecoveries");

            migrationBuilder.RenameIndex(
                name: "IX_passwordRecoveries_UserGuid",
                table: "PasswordRecoveries",
                newName: "IX_PasswordRecoveries_UserGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordRecoveries",
                table: "PasswordRecoveries",
                column: "Token");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordRecoveries_Users_UserGuid",
                table: "PasswordRecoveries",
                column: "UserGuid",
                principalTable: "Users",
                principalColumn: "UserGuid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordRecoveries_Users_UserGuid",
                table: "PasswordRecoveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordRecoveries",
                table: "PasswordRecoveries");

            migrationBuilder.RenameTable(
                name: "PasswordRecoveries",
                newName: "passwordRecoveries");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordRecoveries_UserGuid",
                table: "passwordRecoveries",
                newName: "IX_passwordRecoveries_UserGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_passwordRecoveries",
                table: "passwordRecoveries",
                column: "Token");

            migrationBuilder.AddForeignKey(
                name: "FK_passwordRecoveries_Users_UserGuid",
                table: "passwordRecoveries",
                column: "UserGuid",
                principalTable: "Users",
                principalColumn: "UserGuid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
