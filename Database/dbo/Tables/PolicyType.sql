CREATE TABLE [dbo].[PolicyType]
(
	[PolicyTypeId] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(200) NULL, 
    [SortOrder] INT NOT NULL	
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Determines sorting order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Type Description',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyType',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Type Name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyType',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary key for Policy Type Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyType',
    @level2type = N'COLUMN',
    @level2name = N'PolicyTypeId'