using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class GroupTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupTopic",
                table: "Groups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupTopics",
                columns: table => new
                {
                    Topic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTopics", x => x.Topic);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupTopic",
                table: "Groups",
                column: "GroupTopic");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupTopics_GroupTopic",
                table: "Groups",
                column: "GroupTopic",
                principalTable: "GroupTopics",
                principalColumn: "Topic",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupTopics_GroupTopic",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupTopics");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupTopic",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupTopic",
                table: "Groups");
        }
    }
}
