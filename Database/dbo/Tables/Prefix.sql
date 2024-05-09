CREATE TABLE [dbo].[Prefix]
(
	[PrefixId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PrefixName] NCHAR(10) NOT NULL, 
    [SortOrder] INT NOT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Prefix',
    @level2type = N'COLUMN',
    @level2name = N'PrefixId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Prefix for a user''s name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Prefix',
    @level2type = N'COLUMN',
    @level2name = N'PrefixName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Possible types of user name prefixes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Prefix',
    @level2type = NULL,
    @level2name = NULL
GO

GRANT SELECT
    ON OBJECT::[dbo].[Prefix] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort Order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Prefix',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'