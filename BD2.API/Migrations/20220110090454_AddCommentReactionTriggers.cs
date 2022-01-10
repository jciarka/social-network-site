using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.API.Migrations
{
    public partial class AddCommentReactionTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER OnCommentInsertion ON [dbo].[PostComments]
AFTER INSERT
AS BEGIN 
	UPDATE 
		Posts
	SET 
		CommentsCount = CommentsCount + 1,
		LastCommentDate = GETDATE()
	FROM
		inserted i
	WHERE
		i.PostId = Posts.Id
END
GO

CREATE OR ALTER TRIGGER OnCommentDeletion ON [dbo].[PostComments]
AFTER DELETE
AS BEGIN 
	UPDATE 
		Posts
	SET 
		CommentsCount = CommentsCount - 1
	FROM
		deleted i
	WHERE
		i.PostId = Posts.Id
END
GO

CREATE OR ALTER TRIGGER OnReactionDeletion ON [dbo].[PostReactions]
AFTER INSERT
AS BEGIN 
	UPDATE 
		Posts
	SET 
		PositiveReactionsCount = PositiveReactionsCount + 1,
		LastReactionDate = GETDATE()
	FROM
		inserted i
	WHERE
		i.PostId = Posts.Id and i.Type = 2

	UPDATE 
		Posts
	SET 
		NegativeReactionCount = NegativeReactionCount + 1,
		LastReactionDate = GETDATE()
	FROM
		inserted i
	WHERE
		i.PostId = Posts.Id and i.Type = 1
END
GO

CREATE OR ALTER TRIGGER OnReactionInsertion ON [dbo].[PostReactions]
AFTER INSERT
AS BEGIN 
	UPDATE 
		Posts
	SET 
		PositiveReactionsCount = PositiveReactionsCount - 1,
		LastReactionDate = GETDATE()
	FROM
		deleted i
	WHERE
		i.PostId = Posts.Id and i.Type = 2

	UPDATE 
		Posts
	SET 
		NegativeReactionCount = NegativeReactionCount - 1,
		LastReactionDate = GETDATE()
	FROM
		inserted i
	WHERE
		i.PostId = Posts.Id and i.Type = 1
END
GO

CREATE OR ALTER TRIGGER OnReactionUpdate ON [dbo].[PostReactions]
AFTER UPDATE
AS BEGIN 
	UPDATE 
		Posts
	SET 
		PositiveReactionsCount = PositiveReactionsCount + 1,
		NegativeReactionCount = NegativeReactionCount - 1,
		LastReactionDate = GETDATE()
	FROM
		inserted i inner join deleted d on i.AccountId = d.AccountId and i.AccountId = d.PostId
	WHERE
		i.PostId = Posts.Id and i.Type = 2 and d.Type = 1

	UPDATE 
		Posts
	SET 
		PositiveReactionsCount = PositiveReactionsCount - 1,
		NegativeReactionCount = NegativeReactionCount + 1,
		LastReactionDate = GETDATE()
	FROM
		inserted i inner join deleted d on i.AccountId = d.AccountId and i.AccountId = d.PostId
	WHERE
		i.PostId = Posts.Id and i.Type = 1 and d.Type = 2
END
GO

-- UPDATE existing counts
UPDATE Posts 
SET
	CommentsCount = UP.count
FROM 
	(
		SELECT 
			PostId,
			COUNT(*) as count
		FROM
			PostComments
		GROUP BY
			PostId
	) UP
WHERE
	UP.PostId = Posts.Id
GO
 

UPDATE Posts 
SET
	PositiveReactionsCount = UP.positive,
	NegativeReactionCount = UP.negative

FROM 
	(
		SELECT 
			PostId,
			COUNT(CASE WHEN Type = 1 THEN 1 END) as negative,
			COUNT(CASE WHEN Type = 2 THEN 1 END) as positive
		FROM
			PostReactions
		GROUP BY
			PostId
	) UP
WHERE
	UP.PostId = Posts.Id
GO
");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
