CREATE TABLE [dbo].[PanelStage]
(
	[PanelStageId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SessionPanelId] INT NOT NULL, 
    [ReviewStageId] INT NOT NULL, 
	[StageOrder] INT NOT NULL,
	[WorkflowId] INT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelStage_Panel] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId]), 
    CONSTRAINT [FK_PanelStage_ReviewStage] FOREIGN KEY ([ReviewStageId]) REFERENCES [ReviewStage]([ReviewStageId]), 
    CONSTRAINT [FK_PanelStage_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow]([WorkflowId])
)

GO

CREATE INDEX [IX_PanelStage_SessionPanelId_StageOrder] ON [dbo].[PanelStage] ([SessionPanelId], [StageOrder])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifer for a panel''s review stage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStage',
    @level2type = N'COLUMN',
    @level2name = N'PanelStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stages in which applications for a given panel are expected to participate',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStage',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStage',
    @level2type = N'COLUMN',
    @level2name = N'SessionPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review stage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStage',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The order in which the stage occurs on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStage',
    @level2type = N'COLUMN',
    @level2name = N'StageOrder'
GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The default workflow for applications on this stage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStage',
    @level2type = N'COLUMN',
    @level2name = N'WorkflowId'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelStage] TO [web-p2rmis]
    AS [dbo];