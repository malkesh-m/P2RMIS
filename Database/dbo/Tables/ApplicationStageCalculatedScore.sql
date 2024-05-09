CREATE TABLE [dbo].[ApplicationStageCalculatedScore]
(
	[ApplicationStageCalculatedScoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationStageId] INT NOT NULL, 
    [MechanismTemplateElementId] INT NOT NULL, 
    [AverageScore] DECIMAL(18, 1) NULL, 
    [StandardDeviation] DECIMAL(18, 1) NULL,
    [CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationStageCalculatedScore_ApplicationStageId] FOREIGN KEY ([ApplicationStageId]) REFERENCES [ApplicationStage]([ApplicationStageId]), 
    CONSTRAINT [FK_ApplicationStageCalculatedScore_MechanismTemplateElementId] FOREIGN KEY ([MechanismTemplateElementId]) REFERENCES [MechanismTemplateElement]([MechanismTemplateElementId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Application stage identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStageCalculatedScore',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Mechanism template element identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStageCalculatedScore',
    @level2type = N'COLUMN',
    @level2name = N'MechanismTemplateElementId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Average score for the criteria',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStageCalculatedScore',
    @level2type = N'COLUMN',
    @level2name = N'AverageScore'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Standard deviation of the criteria',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationStageCalculatedScore',
    @level2type = N'COLUMN',
    @level2name = N'StandardDeviation'
GO

CREATE INDEX [IX_ApplicationStageCalculatedScore_ApplicationStageId_MechanismTemplateElementId] ON [dbo].[ApplicationStageCalculatedScore] ([ApplicationStageId],[MechanismTemplateElementId])
