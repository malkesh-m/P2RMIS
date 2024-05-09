CREATE TABLE [dbo].[ProfileType]
(
	[ProfileTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProfileTypeName] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Profile Type identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProfileType',
    @level2type = N'COLUMN',
    @level2name = N'ProfileTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Profile Type Name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProfileType',
    @level2type = N'COLUMN',
    @level2name = 'ProfileTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type sort order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProfileType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'