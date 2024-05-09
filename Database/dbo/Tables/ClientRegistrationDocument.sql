CREATE TABLE [dbo].[ClientRegistrationDocument]
(
	[ClientRegistrationDocumentId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientRegistrationId] INT NOT NULL,
	[RegistrationDocumentTypeId] INT NOT NULL,
    [DocumentName] VARCHAR(30) NOT NULL, 
	[DocumentAbbreviation] VARCHAR(10) NOT NULL DEFAULT 'TBD',
    [SortOrder] INT NOT NULL, 
    [DocumentRoute] VARCHAR(20) NOT NULL, 
    [RequiredFlag] BIT NOT NULL DEFAULT 0, 
    [DocumentVersion] INT NOT NULL, 
    [DocumentUpdateDate] datetime2(0) NOT NULL DEFAULT '9/16/2015', 
    [ConfirmationText] VARCHAR(8000) NULL, 
    [ReportFileName] VARCHAR(50) NULL, 
    CONSTRAINT [FK_ClientRegistrationDocument_ClientRegistration] FOREIGN KEY ([ClientRegistrationId]) REFERENCES [ClientRegistration]([ClientRegistrationId]), 
    CONSTRAINT [FK_ClientRegistrationDocument_RegistrationDocumentType] FOREIGN KEY ([RegistrationDocumentTypeId]) REFERENCES [RegistrationDocumentType]([RegistrationDocumentTypeId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'ClientRegistrationDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'ClientRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The display name for the registration document in the system',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The order in which the registration document occurs',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The route or key value used to associate the document to a piece of system functionality',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentRoute'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the document is required in the sense that it blocks progress if incomplete',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'RequiredFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Registration documents associated with a document set',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The version of the registration document for the client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentVersion'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the client registration document was last updated',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentUpdateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Confirmation text displayed to the user to confirm completion of the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmationText'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientRegistrationDocument] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a registration document type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationDocumentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the SSRS report file without extension for generating the word version of a document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'ReportFileName'