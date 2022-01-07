using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class AbusementsStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntries_Chats_ChatId1",
                table: "ChatEntries");

            migrationBuilder.DropIndex(
                name: "IX_ChatEntries_ChatId1",
                table: "ChatEntries");

            migrationBuilder.DropColumn(
                name: "ChatId1",
                table: "ChatEntries");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PostAbusements",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PostAbusements");

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
        }
    }
}
