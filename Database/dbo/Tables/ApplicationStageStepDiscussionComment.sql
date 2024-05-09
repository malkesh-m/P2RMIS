CREATE TABLE [dbo].[ApplicationStageStepDiscussionComment]
(
	[ApplicationStageStepDiscussionCommentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationStageStepDiscussionId] INT NOT NULL, 
    [Comment] NVARCHAR(MAX) NOT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationStageStepDiscussionComment_ApplicationStageStepDiscussion] FOREIGN KEY ([ApplicationStageStepDiscussionId]) REFERENCES [ApplicationStageStepDiscussion]([ApplicationStageStepDiscussionId]), 
    CONSTRAINT [FK_ApplicationStageStepDiscussionComment_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_ApplicationStageStepDiscussionComment_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [User]([UserId]), 
)

GO

CREATE INDEX [IX_ApplicationStageStepDiscussionComment_ApplicationStageStepDiscussionId] ON [dbo].[ApplicationStageStepDiscussionComment] ([ApplicationStageStepDiscussionId])
