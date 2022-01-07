using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class CommentsAndReacitonsNotificationsProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"

CREATE OR ALTER PROCEDURE GetCommentsNotifications 
	@ownerId uniqueidentifier
as
BEGIN
	SELECT 
		p.Id as PostId,
		p.GroupId as GorupId,
		p.Title as PostTitle,
		a.Firstname as Firstname,
		a.Lastname as Lastname,
		c.Text as text,
		c.CommentDate as CommentDate

	FROM
		PostComments c 
		join Posts p on c.PostId = p.Id
		join AspNetUsers a on a.Id = c.AccountId
	WHERE
		p.OwnerId = @ownerId and
		c.CommentDate > p.LastOwnerViewDate
END
GO

CREATE OR ALTER PROCEDURE GetReactionsNotifications 
	@ownerId uniqueidentifier
as
BEGIN
	SELECT 
		p.Id as PostId,
		p.GroupId as GorupId,
		p.Title as PostTitle,
		a.Firstname as Firstname,
		a.Lastname as Lastname,
		r.ReactionDate as ReactionDate,
		r.Type as Type

	FROM
		PostReactions r 
		join Posts p on r.PostId = p.Id
		join AspNetUsers a on a.Id = r.AccountId
	WHERE
		p.OwnerId = @ownerId and
		r.ReactionDate > p.LastOwnerViewDate
END
GO

");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

DROP PROCEDURE GetCommentsNotifications
GO

DROP PROCEDURE GetReactionsNotifications
GO

");
        }
    }
}
