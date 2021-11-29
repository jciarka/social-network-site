using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class DictsKeyChangePartTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupsLimit",
                table: "Packets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacketPeriod",
                table: "Packets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeopleLimit",
                table: "Packets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PacketGroupsLimits",
                columns: table => new
                {
                    GroupsLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketGroupsLimits", x => x.GroupsLimit);
                });

            migrationBuilder.CreateTable(
                name: "PacketPeopleLimits",
                columns: table => new
                {
                    PeopleLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketPeopleLimits", x => x.PeopleLimit);
                });

            migrationBuilder.CreateTable(
                name: "PacketPeriods",
                columns: table => new
                {
                    MonthsPeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketPeriods", x => x.MonthsPeriod);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packets_GroupsLimit",
                table: "Packets",
                column: "GroupsLimit");

            migrationBuilder.CreateIndex(
                name: "IX_Packets_PacketPeriod",
                table: "Packets",
                column: "PacketPeriod");

            migrationBuilder.CreateIndex(
                name: "IX_Packets_PeopleLimit",
                table: "Packets",
                column: "PeopleLimit");

            migrationBuilder.AddForeignKey(
                name: "FK_Packets_PacketGroupsLimits_GroupsLimit",
                table: "Packets",
                column: "GroupsLimit",
                principalTable: "PacketGroupsLimits",
                principalColumn: "GroupsLimit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packets_PacketPeopleLimits_PeopleLimit",
                table: "Packets",
                column: "PeopleLimit",
                principalTable: "PacketPeopleLimits",
                principalColumn: "PeopleLimit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packets_PacketPeriods_PacketPeriod",
                table: "Packets",
                column: "PacketPeriod",
                principalTable: "PacketPeriods",
                principalColumn: "MonthsPeriod",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packets_PacketGroupsLimits_GroupsLimit",
                table: "Packets");

            migrationBuilder.DropForeignKey(
                name: "FK_Packets_PacketPeopleLimits_PeopleLimit",
                table: "Packets");

            migrationBuilder.DropForeignKey(
                name: "FK_Packets_PacketPeriods_PacketPeriod",
                table: "Packets");

            migrationBuilder.DropTable(
                name: "PacketGroupsLimits");

            migrationBuilder.DropTable(
                name: "PacketPeopleLimits");

            migrationBuilder.DropTable(
                name: "PacketPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Packets_GroupsLimit",
                table: "Packets");

            migrationBuilder.DropIndex(
                name: "IX_Packets_PacketPeriod",
                table: "Packets");

            migrationBuilder.DropIndex(
                name: "IX_Packets_PeopleLimit",
                table: "Packets");

            migrationBuilder.DropColumn(
                name: "GroupsLimit",
                table: "Packets");

            migrationBuilder.DropColumn(
                name: "PacketPeriod",
                table: "Packets");

            migrationBuilder.DropColumn(
                name: "PeopleLimit",
                table: "Packets");
        }
    }
}
