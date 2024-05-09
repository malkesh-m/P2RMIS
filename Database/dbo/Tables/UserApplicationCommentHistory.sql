CREATE TABLE [dbo].[UserApplicationCommentHistory] (
    [CommentID]     INT            NOT NULL,
    [UserID]        INT            NOT NULL,
    [ApplicationID] NVARCHAR (12)  NULL,
    [Comments]      NVARCHAR (MAX) NULL,
    [CreatedBy]     INT            NULL,
    [CreatedDate]   DATETIME       NULL,
    [ModifiedBy]    INT            NULL,
    [ModifiedDate]  DATETIME       NULL,
    [DeletedDate]   DATETIME       NULL,
    [CommentLkpID]  INT            CONSTRAINT [DF_UserApplicationCommentHistory_CommentLkpID] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_UserApplicationCommentHistory] PRIMARY KEY CLUSTERED ([CommentID] ASC)
);

