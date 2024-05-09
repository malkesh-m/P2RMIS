CREATE TABLE [dbo].[CommentType] (
    [CommentTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [CommentTypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LookupCommentType] PRIMARY KEY CLUSTERED ([CommentTypeID] ASC)
);

