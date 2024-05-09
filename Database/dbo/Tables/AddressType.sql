CREATE TABLE [dbo].[AddressType]
(
	[AddressTypeId] INT NOT NULL PRIMARY KEY, 
    [AddressTypeName] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an address type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AddressType',
    @level2type = N'COLUMN',
    @level2name = N'AddressTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name for an address type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AddressType',
    @level2type = N'COLUMN',
    @level2name = N'AddressTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Possible types of user addresses',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AddressType',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[AddressType] TO [web-p2rmis]
    AS [dbo];