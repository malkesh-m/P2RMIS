CREATE TABLE [dbo].[ApplicationStageStep]
(
	[ApplicationStageStepId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationStageId] INT NOT NULL, 
    [PanelStageStepId] INT NOT NULL, 
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationStageStep_ApplicationStage] FOREIGN KEY ([ApplicationStageId]) REFERENCES [ApplicationStage]([ApplicationStageId]), 
    CONSTRAINT [FK_ApplicationStageStep_PanelStageStep] FOREIGN KEY ([PanelStageStepId]) REFERENCES [PanelStageStep]([PanelStageStepId]), 
)

GO

CREATE INDEX [IX_ApplicationStageStep_ApplicationStageId] ON [dbo].[ApplicationStageStep] ([ApplicationStageId],[DeletedFlag])

GO

CREATE INDEX [IX_ApplicationStageStep_PanelStageStepId_ApplicationStageId_DeletedFlag] ON [dbo].[ApplicationStageStep] ([PanelStageStepId],[ApplicationStageId],[DeletedFlag])
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationStageStep] TO [web-p2rmis]
    AS [dbo];
GO
