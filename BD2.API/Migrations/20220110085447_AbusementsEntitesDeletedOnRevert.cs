using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class AbusementsEntitesDeletedOnRevert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntries_Chats_ChatId1",
                table: "ChatEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_Images_ImageId",
                table: "PostImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_Posts_PostId",
                table: "PostImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_Posts_PostId",
                table: "PostReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews");

            migrationBuilder.DropIndex(
                name: "IX_ChatEntries_ChatId1",
                table: "ChatEntries");

            migrationBuilder.DropColumn(
                name: "ChatId1",
                table: "ChatEntries");

            migrationBuilder.CreateTable(
                name: "PostAbusements",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AbusementDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CheckedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAbusements", x => new { x.AccountId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostAbusements_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostAbusements_AspNetUsers_CheckedById",
                        column: x => x.CheckedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostAbusements_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostAbusements_CheckedById",
                table: "PostAbusements",
                column: "CheckedById");

            migrationBuilder.CreateIndex(
                name: "IX_PostAbusements_PostId",
                table: "PostAbusements",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_Images_ImageId",
                table: "PostImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_Posts_PostId",
                table: "PostImages",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_Posts_PostId",
                table: "PostReactions",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_Images_ImageId",
                table: "PostImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_Posts_PostId",
                table: "PostImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_Posts_PostId",
                table: "PostReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews");

            migrationBuilder.DropTable(
                name: "PostAbusements");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatId1",
                table: "ChatEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntries_ChatId1",
                table: "ChatEntries",
                column: "ChatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntries_Chats_ChatId1",
                table: "ChatEntries",
                column: "ChatId1",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_Images_ImageId",
                table: "PostImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_Posts_PostId",
                table: "PostImages",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_Posts_PostId",
                table: "PostReactions",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
