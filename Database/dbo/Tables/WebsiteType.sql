CREATE TABLE [dbo].[WebsiteType]
(
	[WebsiteTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WebsiteType] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Website Type identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WebsiteType',
    @level2type = N'COLUMN',
    @level2name = N'WebsiteTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Website Type (e.g. Primary, Secondary)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WebsiteType',
    @level2type = N'COLUMN',
    @level2name = N'WebsiteType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort Order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WebsiteType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'