CREATE TABLE [dbo].[ClientRegistrationDocumentItem]
(
	[ClientRegistrationDocumentItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientRegistrationDocumentId] INT NOT NULL, 
    [RegistrationDocumentItemId] INT NOT NULL, 
    [RequiredFlag] BIT NOT NULL DEFAULT 0, 
    [RequiredMessage] VARCHAR(500) NULL, 
    CONSTRAINT [FK_ClientRegistrationDocumentItem_ClientRegistrationDocument] FOREIGN KEY ([ClientRegistrationDocumentId]) REFERENCES [ClientRegistrationDocument]([ClientRegistrationDocumentId]), 
    CONSTRAINT [FK_ClientRegistrationDocumentItem_RegistrationDocumentItem] FOREIGN KEY ([RegistrationDocumentItemId]) REFERENCES [RegistrationDocumentItem]([RegistrationDocumentItemId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client registration document item',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'ClientRegistrationDocumentItemId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'ClientRegistrationDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a form item within a client registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationDocumentItemId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the item is required to proceed to the next tab',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'RequiredFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Message to display if a required item is not present',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'RequiredMessage'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientRegistrationDocumentItem] TO [web-p2rmis]
    AS [dbo];