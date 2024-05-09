CREATE TABLE [dbo].[PanelUserRegistrationDocumentItem]
(
	[PanelUserRegistrationDocumentItemId] INT NOT NULL PRIMARY KEY IDENTITY,
	[PanelUserRegistrationDocumentId] INT NOT NULL,
	[RegistrationDocumentItemId] INT NOT NULL,
	[Value] VARCHAR(8000) NULL, 
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL,
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelUserRegistrationDocumentItem_RegistrationDocumentItem] FOREIGN KEY ([RegistrationDocumentItemId]) REFERENCES [RegistrationDocumentItem]([RegistrationDocumentItemId]), 
    CONSTRAINT [FK_PanelUserRegistrationDocumentItem_PanelUserRegistrationDocument] FOREIGN KEY ([PanelUserRegistrationDocumentId]) REFERENCES [PanelUserRegistrationDocument]([PanelUserRegistrationDocumentId]), 
)

GO

CREATE INDEX [IX_PanelUserRegistrationDocumentItem_PanelUserRegistrationDocumentId_RegistrationDocumentItemId] ON [dbo].[PanelUserRegistrationDocumentItem] ([PanelUserRegistrationDocumentId],[RegistrationDocumentItemId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a piece of registration data within a user''s registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserRegistrationDocumentItemId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel user''s registration instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserRegistrationDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a registration document item',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationDocumentItemId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stored value provided by the user for a document item',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentItem',
    @level2type = N'COLUMN',
    @level2name = N'Value'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Data items associated with a registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentItem',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelUserRegistrationDocumentItem] TO [web-p2rmis]
    AS [dbo];
