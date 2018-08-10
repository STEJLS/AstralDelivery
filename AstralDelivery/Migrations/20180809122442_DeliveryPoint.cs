using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralDelivery.Migrations
{
    public partial class DeliveryPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryPointGuid",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DeliveryPoints",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Building = table.Column<int>(nullable: false),
                    Corpus = table.Column<string>(nullable: true),
                    Office = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPoints", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "WorkTime",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Begin = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false),
                    DeliveryPointGuid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTime", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_WorkTime_DeliveryPoints_DeliveryPointGuid",
                        column: x => x.DeliveryPointGuid,
                        principalTable: "DeliveryPoints",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeliveryPointGuid",
                table: "Users",
                column: "DeliveryPointGuid");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTime_DeliveryPointGuid",
                table: "WorkTime",
                column: "DeliveryPointGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DeliveryPoints_DeliveryPointGuid",
                table: "Users",
                column: "DeliveryPointGuid",
                principalTable: "DeliveryPoints",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DeliveryPoints_DeliveryPointGuid",
                table: "Users");

            migrationBuilder.DropTable(
                name: "WorkTime");

            migrationBuilder.DropTable(
                name: "DeliveryPoints");

            migrationBuilder.DropIndex(
                name: "IX_Users_DeliveryPointGuid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeliveryPointGuid",
                table: "Users");
        }
    }
}
