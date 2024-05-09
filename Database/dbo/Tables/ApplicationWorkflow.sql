CREATE TABLE [dbo].[ApplicationWorkflow]
(
	[ApplicationWorkflowId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
	[WorkflowId] INT NOT NULL,
	[ApplicationStageId] INT NOT NULL,
	[ApplicationTemplateId] INT NOT NULL,
	[PanelUserAssignmentId] INT NULL,
    [ApplicationWorkflowName] VARCHAR(50) NOT NULL, 
    [DateAssigned] datetime2(0) NULL, 
    [DateClosed] datetime2(0) NULL, 
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationWorkflow_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow]([WorkflowId]), 
	CONSTRAINT [FK_ApplicationWorkflow_ApplicationTemplate] FOREIGN KEY ([ApplicationTemplateId]) REFERENCES [ApplicationTemplate]([ApplicationTemplateId]), 
    CONSTRAINT [FK_ApplicationWorkflow_ApplicationStage] FOREIGN KEY ([ApplicationStageId]) REFERENCES [ApplicationStage]([ApplicationStageId]), 
    CONSTRAINT [FK_ApplicationWorkflow_PanelUserAssignment] FOREIGN KEY ([PanelUserAssignmentId]) REFERENCES [PanelUserAssignment]([PanelUserAssignmentId]) 
)

GO

CREATE INDEX [IX_ApplicationWorkflow_ApplicationTemplateId] ON [dbo].[ApplicationWorkflow] ([ApplicationTemplateId],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'PanelUserAssignment identifier for workflows dedicated to a single user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflow',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserAssignmentId'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationWorkflow] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_ApplicationWorkflow_ApplicationStageId_DeletedFlag] ON [dbo].[ApplicationWorkflow] ([ApplicationStageId], [DeletedFlag])

GO

CREATE INDEX [IX_ApplicationWorkflow_PanelUserAssignmentId] ON [dbo].[ApplicationWorkflow] ([PanelUserAssignmentId], [ApplicationStageId], [DeletedFlag])
