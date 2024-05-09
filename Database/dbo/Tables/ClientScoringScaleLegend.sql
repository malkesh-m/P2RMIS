CREATE TABLE [dbo].[ClientScoringScaleLegend]
(
	[ClientScoringScaleLegendId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientId] INT NOT NULL,
    [LegendName] VARCHAR(20) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display name for the legend',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegend',
    @level2type = N'COLUMN',
    @level2name = N'LegendName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegend',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a scoring scale legend',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegend',
    @level2type = N'COLUMN',
    @level2name = N'ClientScoringScaleLegendId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Provides a scoring legend to guide reviewers numeric scores',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegend',
    @level2type = NULL,
    @level2name = NULL