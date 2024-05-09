CREATE TABLE [dbo].[ApplicationStage]
(
	[ApplicationStageId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelApplicationId] INT NOT NULL, 
    [ReviewStageId] INT NOT NULL, 
	[StageOrder] INT NOT NULL,
	[ActiveFlag] BIT NOT NULL DEFAULT 1,
	[AssignmentVisibilityFlag] BIT NOT NULL DEFAULT 0,
	[AssignmentReleaseDate] datetime2(0) NULL,
    [CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationStage_PanelApplication] FOREIGN KEY ([PanelApplicationId]) REFERENCES [PanelApplication]([PanelApplicationId]), 
    CONSTRAINT [FK_ApplicationStage_ReviewStage] FOREIGN KEY ([ReviewStageId]) REFERENCES [ReviewStage]([ReviewStageId])
)

GO

CREATE INDEX [IX_ApplicationStage_PanelApplicationId_StageOrder] ON [dbo].[ApplicationStage] ([PanelApplicationId], [StageOrder])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a stage of review for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStage',
    @level2type = N'COLUMN',
    @level2name = 'ApplicationStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application panel assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStage',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the system review stage ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStage',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The order in which the stage is expected to occur',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStage',
    @level2type = N'COLUMN',
    @level2name = N'StageOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the stage is currently expected to occur for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStage',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Application''s collection of review stages and information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStage',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether assignments for this stage are available to reviewers',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStage',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentVisibilityFlag'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationStage] TO [web-p2rmis]
    AS [dbo];