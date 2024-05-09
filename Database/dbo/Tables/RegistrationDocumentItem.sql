CREATE TABLE [dbo].[RegistrationDocumentItem]
(
	[RegistrationDocumentItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemName] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a possible registration document item',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationDocumentItemId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Human readable description of the item',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'ItemName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Possible registration document items',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RegistrationDocumentItem',
    @level2type = NULL,
    @level2name = NULL