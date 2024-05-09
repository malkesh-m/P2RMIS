CREATE TABLE [dbo].[ApplicationStageStepDiscussion]
(
	[ApplicationStageStepDiscussionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationStageStepId] INT NOT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationStageStepDiscussion_ApplicationStageStepId] FOREIGN KEY ([ApplicationStageStepId]) REFERENCES [ApplicationStageStep]([ApplicationStageStepId]), 
)

GO

CREATE INDEX [IX_ApplicationStageStepDiscussion_ApplicationStageStepId_DeletedFlag] ON [dbo].[ApplicationStageStepDiscussion] ([ApplicationStageStepId],[DeletedFlag])
