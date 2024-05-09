CREATE TABLE [dbo].[PanelUserRegistrationDocument]
(
	[PanelUserRegistrationDocumentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelUserRegistrationId] INT NOT NULL, 
	[ClientRegistrationDocumentId] INT NOT NULL,
    [DocumentFile] VARCHAR(MAX) NULL, 
    [DateCompleted] datetime2(0) NULL,
	[SignedBy] INT NULL, 
    [DateSigned] datetime2(0) NULL, 
	[SignedOfflineFlag] BIT DEFAULT 0,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL,
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelUserRegistrationDocument_PanelUserRegistrationId] FOREIGN KEY ([PanelUserRegistrationId]) REFERENCES [PanelUserRegistration]([PanelUserRegistrationId]), 
    CONSTRAINT [FK_PanelUserRegistrationDocument_ClientRegistrationDocument] FOREIGN KEY ([ClientRegistrationDocumentId]) REFERENCES [ClientRegistrationDocument]([ClientRegistrationDocumentId]), 
)

GO

CREATE INDEX [IX_PanelUserRegistrationDocument_PanelUserRegistrationId_DateSigned] ON [dbo].[PanelUserRegistrationDocument] ([PanelUserRegistrationId],[ClientRegistrationDocumentId],[DateSigned])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel user''s registration instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'ClientRegistrationDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'HTML format of the registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentFile'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the document was last progressed through',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'DateCompleted'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the document was signed/confirmed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'DateSigned'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel user''s registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserRegistrationDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User''s registration document for a panel registration.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelUserRegistrationDocument] TO [web-p2rmis]
    AS [dbo];

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Who the document was signed/confirmed by',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'SignedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the document was signed offline. If offline, signed by is the person who confirmed rather than actually signed.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocument',
    @level2type = N'COLUMN',
    @level2name = N'SignedOfflineFlag'