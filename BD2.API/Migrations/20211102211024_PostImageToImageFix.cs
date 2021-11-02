using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class PostImageToImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_Posts_PostId1",
                table: "PostImages");

            migrationBuilder.DropIndex(
                name: "IX_PostImages_PostId1",
                table: "PostImages");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "PostImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostId1",
                table: "PostImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostImages_PostId1",
                table: "PostImages",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_Posts_PostId1",
                table: "PostImages",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
