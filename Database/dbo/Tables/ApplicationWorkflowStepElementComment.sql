CREATE TABLE [dbo].[ApplicationWorkflowStepElementComment]
(
	[ApplicationWorkflowStepElementCommentId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY NONCLUSTERED, 
	[ApplicationWorkflowStepElementId] INT NOT NULL,
    [UserId] INT NOT NULL, 
    [Comment] VARCHAR(MAX) NOT NULL,
	[CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    CONSTRAINT [FK_ApplicationWorkflowStepElementComment_ApplicationWorkflowStepElement] FOREIGN KEY ([ApplicationWorkflowStepElementId]) REFERENCES [ApplicationWorkflowStepElement]([ApplicationWorkflowStepElementId]), 
    CONSTRAINT [FK_ApplicationWorkflowStepElementComment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)

GO

CREATE CLUSTERED INDEX [IX_ApplicationWorkflowStepElementComment_ApplicationWorkflowStepElementId_UserId] ON [dbo].[ApplicationWorkflowStepElementComment] ([ApplicationWorkflowStepElementId],[UserId])

GO

CREATE INDEX [IX_ApplicationWorkflowStepElementComment_UserId] ON [dbo].[ApplicationWorkflowStepElementComment] ([UserId])
