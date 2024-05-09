CREATE TABLE [dbo].[WorkflowMechanism]
(
	[WorkflowMechanismId] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY, 
    [WorkflowId] INT NOT NULL, 
    [MechanismId] INT NOT NULL, 
    [ReviewStatusId] INT NULL, 
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    CONSTRAINT [UN_WorkflowMechanism_WorkflowId_MechanismId_ReviewStatusId] UNIQUE ([WorkflowId], [MechanismId], [ReviewStatusId]), 
    CONSTRAINT [FK_WorkflowMechanism_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow]([WorkflowId]), 
    CONSTRAINT [FK_WorkflowMechanism_ReviewStatus] FOREIGN KEY ([ReviewStatusId]) REFERENCES [ReviewStatus]([ReviewStatusId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for a workflow mechanism assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WorkflowMechanism',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowMechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Workflow definition id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WorkflowMechanism',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Award mechanism id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WorkflowMechanism',
    @level2type = N'COLUMN',
    @level2name = N'MechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Mechanism''s applications review status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WorkflowMechanism',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStatusId'
GO

CREATE CLUSTERED INDEX [IX_WorkflowMechanism_MechanismId_ReviewStatusId_WorkflowId] ON [dbo].[WorkflowMechanism] ([MechanismId], [ReviewStatusId], [WorkflowId])

GO

CREATE INDEX [IX_WorkflowMechanism_WorkflowId] ON [dbo].[WorkflowMechanism] ([WorkflowId])
