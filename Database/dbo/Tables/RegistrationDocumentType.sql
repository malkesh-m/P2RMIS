CREATE TABLE [dbo].[RegistrationDocumentType]
(
	[RegistrationDocumentTypeId] INT NOT NULL PRIMARY KEY, 
    [RegistrationDocumentName] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system registration document type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RegistrationDocumentType',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationDocumentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RegistrationDocumentType',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationDocumentName'