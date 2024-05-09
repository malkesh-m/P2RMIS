CREATE TABLE [dbo].[UserApplicationComment] (
    [UserApplicationCommentID]     INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NOT NULL,
	[PanelApplicationId] INT	NOT NULL , --TODO: REFACTOR THIS AND ALL THE CODE
    [Comments]      NVARCHAR (MAX) NULL,
	[CommentTypeID]  INT            CONSTRAINT [DF_UserApplicationComment_CommentLkpID] DEFAULT ((3)) NOT NULL,
    [CreatedBy]     INT            NULL,
    [CreatedDate]   datetime2(0)       NULL,
    [ModifiedBy]    INT            NULL,
    [ModifiedDate]  datetime2(0)       NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [PK_UserApplicationComment] PRIMARY KEY CLUSTERED ([UserApplicationCommentID] ASC),
    CONSTRAINT [FK_User_ApplicationComment] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]),
    CONSTRAINT [FK_UserApplicationComment_LookupCommentType] FOREIGN KEY ([CommentTypeID]) REFERENCES [dbo].[CommentType] ([CommentTypeID]), 
    CONSTRAINT [FK_UserApplicationComment_PanelApplicationId] FOREIGN KEY ([PanelApplicationId]) REFERENCES [PanelApplication]([PanelApplicationId])
);



GO
GRANT SELECT
    ON OBJECT::[dbo].[UserApplicationComment] TO [web-p2rmis]
    AS [dbo];


GO

CREATE INDEX [IX_UserApplicationComment_PanelApplicationId_CommentTypeID] ON [dbo].[UserApplicationComment] ([PanelApplicationId],[CommentTypeID])

GO

GO

CREATE UNIQUE INDEX [IX_UserApplicationComment_UnassignedReviewerComments_UN] ON [dbo].[UserApplicationComment] ([PanelApplicationId],[UserId]) WHERE CommentTypeId = 5 AND DeletedFlag = 0
