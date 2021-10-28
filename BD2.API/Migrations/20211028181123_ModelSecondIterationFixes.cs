using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class ModelSecondIterationFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Images");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ViewDate",
                table: "PostViews",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReactionDate",
                table: "PostReactions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentDate",
                table: "PostComments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReactionDate",
                table: "PostReactions");

            migrationBuilder.DropColumn(
                name: "CommentDate",
                table: "PostComments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ViewDate",
                table: "PostViews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
