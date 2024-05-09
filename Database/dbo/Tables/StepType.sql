CREATE TABLE [dbo].[StepType]
(
	[StepTypeId] INT NOT NULL PRIMARY KEY, 
    [StepTypeName] VARCHAR(40) NOT NULL, 
	[ReviewStageId] INT NOT NULL DEFAULT 1,
    [CleanMarkupFlag] BIT NOT NULL DEFAULT 0, 
    [SortOrder] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_StepType_ReviewStage] FOREIGN KEY ([ReviewStageId]) REFERENCES [ReviewStage]([ReviewStageId])
)

GO
GRANT SELECT
    ON OBJECT::[dbo].[StepType] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a step type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepType',
    @level2type = N'COLUMN',
    @level2name = N'StepTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of a step type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepType',
    @level2type = N'COLUMN',
    @level2name = N'StepTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the step should have clean review markup prior to startr',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepType',
    @level2type = N'COLUMN',
    @level2name = N'CleanMarkupFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the review stage in which the step type is applicable',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepType',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the step should occur when appearing in lists',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'