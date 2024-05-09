CREATE TABLE [dbo].[AlternateContactType]
(
	[AlternateContactTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AlternateContactType] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Alternate Contact Type identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AlternateContactType',
    @level2type = N'COLUMN',
    @level2name = N'AlternateContactTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Alternate Contact Type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AlternateContactType',
    @level2type = N'COLUMN',
    @level2name = N'AlternateContactType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort Order of contact types',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AlternateContactType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
