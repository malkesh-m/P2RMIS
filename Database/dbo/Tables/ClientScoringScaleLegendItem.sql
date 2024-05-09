CREATE TABLE [dbo].[ClientScoringScaleLegendItem]
(
	[ClientScoringScaleLegendItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientScoringScaleLegendId] INT NOT NULL,
    [LegendItemLabel] VARCHAR(45) NOT NULL, 
    [HighValueLabel] VARCHAR(5) NOT NULL, 
    [LowValueLabel] VARCHAR(5) NOT NULL, 
    [SortOrder] INT NOT NULL, 
    CONSTRAINT [FK_ClientScoringScaleLegendItem_ClientScoringScaleLegend] FOREIGN KEY ([ClientScoringScaleLegendId]) REFERENCES [ClientScoringScaleLegend]([ClientScoringScaleLegendId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an item in a scoring scale legend',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegendItem',
    @level2type = N'COLUMN',
    @level2name = N'ClientScoringScaleLegendItemId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label providing a description for a score range',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegendItem',
    @level2type = N'COLUMN',
    @level2name = N'LegendItemLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'High value for a score range',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegendItem',
    @level2type = N'COLUMN',
    @level2name = N'HighValueLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Low value for a score range',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegendItem',
    @level2type = N'COLUMN',
    @level2name = N'LowValueLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the legend item should appear',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegendItem',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client scoring scale legend',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegendItem',
    @level2type = N'COLUMN',
    @level2name = N'ClientScoringScaleLegendId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Items used to provide a legend for numeric score values',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientScoringScaleLegendItem',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_ClientScoringScaleLegendItem_ClientScoringId_SortOrder] ON [dbo].[ClientScoringScaleLegendItem] ([ClientScoringScaleLegendId],[SortOrder])
