using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class PacketsEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PacketGroupsLimits",
                columns: table => new
                {
                    GroupsLimit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
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
                        .Annotation("SqlServer:Identity", "1, 1")
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
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketPeriods", x => x.MonthsPeriod);
                });

            migrationBuilder.CreateTable(
                name: "Packets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID ()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    GroupsLimit = table.Column<int>(type: "int", nullable: false),
                    PeopleLimit = table.Column<int>(type: "int", nullable: false),
                    PacketPeriod = table.Column<int>(type: "int", nullable: false),
                    IsValid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packets_PacketGroupsLimits_GroupsLimit",
                        column: x => x.GroupsLimit,
                        principalTable: "PacketGroupsLimits",
                        principalColumn: "GroupsLimit",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Packets_PacketPeopleLimits_PeopleLimit",
                        column: x => x.PeopleLimit,
                        principalTable: "PacketPeopleLimits",
                        principalColumn: "PeopleLimit",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Packets_PacketPeriods_PacketPeriod",
                        column: x => x.PacketPeriod,
                        principalTable: "PacketPeriods",
                        principalColumn: "MonthsPeriod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacketSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID ()"),
                    ExpirationDate = table.Column<DateTime>(type: "date", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacketSubscriptions_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacketSubscriptions_Packets_PacketId",
                        column: x => x.PacketId,
                        principalTable: "Packets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SubscriptionId",
                table: "Groups",
                column: "SubscriptionId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PacketSubscriptions_OwnerId",
                table: "PacketSubscriptions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PacketSubscriptions_PacketId",
                table: "PacketSubscriptions",
                column: "PacketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_PacketSubscriptions_SubscriptionId",
                table: "Groups",
                column: "SubscriptionId",
                principalTable: "PacketSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_PacketSubscriptions_SubscriptionId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "PacketSubscriptions");

            migrationBuilder.DropTable(
                name: "Packets");

            migrationBuilder.DropTable(
                name: "PacketGroupsLimits");

            migrationBuilder.DropTable(
                name: "PacketPeopleLimits");

            migrationBuilder.DropTable(
                name: "PacketPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Groups_SubscriptionId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Groups");
        }
    }
}
