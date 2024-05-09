CREATE TABLE [dbo].[PhoneType]
(
	[PhoneTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PhoneType] VARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL 
)

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Available type of phone numbers a user can include in their profile',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PhoneType',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a type of contact phone number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PhoneType',
    @level2type = N'COLUMN',
    @level2name = N'PhoneTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of contact phone number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PhoneType',
    @level2type = N'COLUMN',
    @level2name = N'PhoneType'
GO

GRANT SELECT
    ON OBJECT::[dbo].[PhoneType] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort Order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PhoneType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'