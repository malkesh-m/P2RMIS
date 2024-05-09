CREATE TABLE [dbo].[SummaryStatementMode]
(
	[SummaryStatementModeId] INT NOT NULL PRIMARY KEY,
	[ModeLabel] VARCHAR(20) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a summary statement editing mode',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryStatementMode',
    @level2type = N'COLUMN',
    @level2name = N'SummaryStatementModeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label for a summary statement editing mode',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryStatementMode',
    @level2type = N'COLUMN',
    @level2name = N'ModeLabel'