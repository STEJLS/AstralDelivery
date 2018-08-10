using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class WorkTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "WorkTime",
                newName: "WorkTimes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DeliveryPoints",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_WorkTimes_DayOfWeek_DeliveryPointGuid",
                table: "WorkTimes",
                columns: new[] { "DayOfWeek", "DeliveryPointGuid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTimes",
                table: "WorkTimes",
                columns: new[] { "DeliveryPointGuid", "DayOfWeek" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTimes_DeliveryPoints_DeliveryPointGuid",
                table: "WorkTimes",
                column: "DeliveryPointGuid",
                principalTable: "DeliveryPoints",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTimes_DeliveryPoints_DeliveryPointGuid",
                table: "WorkTimes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_WorkTimes_DayOfWeek_DeliveryPointGuid",
                table: "WorkTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTimes",
                table: "WorkTimes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DeliveryPoints");

            migrationBuilder.RenameTable(
                name: "WorkTimes",
                newName: "WorkTime");

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
    }
}
