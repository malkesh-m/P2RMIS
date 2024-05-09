CREATE TABLE [dbo].[PolicyRestrictionType]
(
	[PolicyRestrictionTypeId] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(200) NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary key for Restriction Type Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyRestrictionType',
    @level2type = N'COLUMN',
    @level2name = N'PolicyRestrictionTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Restriction Type Name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyRestrictionType',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Restriction Type Desciption',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyRestrictionType',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Determines sorting order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyRestrictionType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'