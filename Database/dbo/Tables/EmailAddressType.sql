CREATE TABLE [dbo].[EmailAddressType]
(
	[EmailAddressTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmailAddressType] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Email Address Type identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'EmailAddressType',
    @level2type = N'COLUMN',
    @level2name = N'EmailAddressTypeId'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type Sort Order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'EmailAddressType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Email Address Type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'EmailAddressType',
    @level2type = N'COLUMN',
    @level2name = N'EmailAddressType'