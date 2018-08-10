using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class relationsDeliveryPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTime_DeliveryPoints_DeliveryPointGuid",
                table: "WorkTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTime",
                table: "WorkTime");

            migrationBuilder.DropIndex(
                name: "IX_WorkTime_DeliveryPointGuid",
                table: "WorkTime");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "WorkTime");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryPointGuid",
                table: "WorkTime",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "WorkTime",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_WorkTime_DayOfWeek_DeliveryPointGuid",
                table: "WorkTime",
                columns: new[] { "DayOfWeek", "DeliveryPointGuid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTime",
                table: "WorkTime",
                columns: new[] { "DeliveryPointGuid", "DayOfWeek" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTime_DeliveryPoints_DeliveryPointGuid",
                table: "WorkTime",
                column: "DeliveryPointGuid",
                principalTable: "DeliveryPoints",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTime_DeliveryPoints_DeliveryPointGuid",
                table: "WorkTime");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_WorkTime_DayOfWeek_DeliveryPointGuid",
                table: "WorkTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTime",
                table: "WorkTime");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "WorkTime");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryPointGuid",
                table: "WorkTime",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "WorkTime",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTime",
                table: "WorkTime",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTime_DeliveryPointGuid",
                table: "WorkTime",
                column: "DeliveryPointGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTime_DeliveryPoints_DeliveryPointGuid",
                table: "WorkTime",
                column: "DeliveryPointGuid",
                principalTable: "DeliveryPoints",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
