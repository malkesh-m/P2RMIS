CREATE TABLE [dbo].[PanelStageStep]
(
	[PanelStageStepId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelStageId] INT NOT NULL, 
    [StepTypeId] INT NOT NULL, 
	[StepName] VARCHAR(50) NOT NULL,
    [StepOrder] INT NOT NULL, 
    [StartDate] datetime2(0) NULL, 
    [EndDate] datetime2(0) NULL, 
    [ReOpenDate] datetime2(0) NULL, 
    [ReCloseDate] datetime2(0) NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelStageStep_PanelStage] FOREIGN KEY ([PanelStageId]) REFERENCES [PanelStage]([PanelStageId]), 
    CONSTRAINT [FK_PanelStageStep_StepType] FOREIGN KEY ([StepTypeId]) REFERENCES [StepType]([StepTypeId])
)

GO

CREATE INDEX [IX_PanelStageStep_PanelStageId_StepOrder] ON [dbo].[PanelStageStep] ([PanelStageId], [StepOrder])

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'A panel stages expected step through which it can send an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel review stage step',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'PanelStageStepId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel review stage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'PanelStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type of application step',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'StepTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the step occurs within the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'StepOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the step becomes available',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the step ends',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the step becomes available again for late submittals',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'ReOpenDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the late submission period closes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelStageStep',
    @level2type = N'COLUMN',
    @level2name = N'ReCloseDate'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelStageStep] TO [web-p2rmis]
    AS [dbo];
