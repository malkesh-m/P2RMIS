CREATE TABLE [dbo].[ApplicationDefaultWorkflow]
(
	[ApplicationDefaultWorkflowId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WorkflowId] INT NULL, 
    [ApplicationId] INT NOT NULL,
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    CONSTRAINT [UN_ApplicationDefaultWorkflow_ApplicationId] UNIQUE ([ApplicationId]), 
    CONSTRAINT [FK_ApplicationDefaultWorkflow_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ApplicationDefaultWorkflow_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow]([WorkflowId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Application for which workflow/template are defaulted',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationDefaultWorkflow',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Workflow to default',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationDefaultWorkflow',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowId'
GO

CREATE INDEX [IX_ApplicationDefaultWorkflow_ApplicationIdWorkflowId] ON [dbo].[ApplicationDefaultWorkflow] ([ApplicationId], [WorkflowId])
