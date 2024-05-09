CREATE TABLE [dbo].[Workflow]
(
	[WorkflowId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
	[ClientId] INT NOT NULL,
	[ReviewStageId] INT NOT NULL,
    [WorkflowName] VARCHAR(50) NOT NULL, 
    [WorkflowDescription] VARCHAR(1000) NOT NULL, 
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_Workflow_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_Workflow_ReviewStage] FOREIGN KEY ([ReviewStageId]) REFERENCES [ReviewStage]([ReviewStageId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for a workflow template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Workflow',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier to designate the client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Workflow',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the workflow template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Workflow',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description to describe the workflow and it''s use',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Workflow',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowDescription'
GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The review stage under which this workflow is available for assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Workflow',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStageId'
GO

CREATE INDEX [IX_Workflow_ClientId_ReviewStageId] ON [dbo].[Workflow] ([ClientId],[ReviewStageId])

GO
GRANT SELECT
    ON OBJECT::[dbo].[Workflow] TO [web-p2rmis]
    AS [dbo];