using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class ChatAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatAccounts_Chats_ChatId1",
                table: "ChatAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAccounts_Groups_GroupId1",
                table: "GroupAccounts");

            migrationBuilder.DropIndex(
                name: "IX_GroupAccounts_GroupId1",
                table: "GroupAccounts");

            migrationBuilder.DropIndex(
                name: "IX_ChatAccounts_ChatId1",
                table: "ChatAccounts");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "GroupAccounts");

            migrationBuilder.DropColumn(
                name: "ChatId1",
                table: "ChatAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Chats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 200);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId1",
                table: "GroupAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Name",
                table: "Chats",
                type: "uniqueidentifier",
                maxLength: 200,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChatId1",
                table: "ChatAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupAccounts_GroupId1",
                table: "GroupAccounts",
                column: "GroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAccounts_ChatId1",
                table: "ChatAccounts",
                column: "ChatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatAccounts_Chats_ChatId1",
                table: "ChatAccounts",
                column: "ChatId1",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAccounts_Groups_GroupId1",
                table: "GroupAccounts",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
