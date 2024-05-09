CREATE TABLE [dbo].[ClientDefaultWorkflow]
(
	[ClientDefaultWorkflowId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [WorkflowId] INT NOT NULL, 
    [ReviewStatusId] INT NULL,
	CONSTRAINT [FK_ClientDefaultWorkflow_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
	CONSTRAINT [FK_ClientDefaultWorkflow_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow]([WorkflowId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'System table which stores default workflow specification for each client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDefaultWorkflow',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for a client workflow assignment.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDefaultWorkflow',
    @level2type = N'COLUMN',
    @level2name = N'ClientDefaultWorkflowId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client idenitifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDefaultWorkflow',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Workflow identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDefaultWorkflow',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Review Status identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDefaultWorkflow',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStatusId'