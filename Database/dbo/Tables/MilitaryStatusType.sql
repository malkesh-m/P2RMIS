CREATE TABLE [dbo].[MilitaryStatusType]
(
	[MilitaryStatusTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StatusType] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Military Status Type identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryStatusType',
    @level2type = N'COLUMN',
    @level2name = N'MilitaryStatusTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Status Type (e.g. Active/Retired)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryStatusType',
    @level2type = N'COLUMN',
    @level2name = N'StatusType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort Order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryStatusType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'