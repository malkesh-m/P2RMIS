CREATE TABLE [dbo].[ApplicationText]
(
	[ApplicationTextId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL, 
    [ClientApplicationTextTypeId] INT NOT NULL, 
    [BodyText] NVARCHAR(MAX) NOT NULL, 
	[AbstractFlag] BIT NOT NULL DEFAULT 0,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationText_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ApplicationText_ClientApplicationTextType] FOREIGN KEY ([ClientApplicationTextTypeId]) REFERENCES [ClientApplicationTextType]([ClientApplicationTextTypeId])
)

GO

CREATE INDEX [IX_ApplicationText_ApplicationId_ApplicationTextTypeId] ON [dbo].[ApplicationText] ([ApplicationId], [ClientApplicationTextTypeId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application text document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationText',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationTextId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationText',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a type of text document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationText',
    @level2type = N'COLUMN',
    @level2name = N'ClientApplicationTextTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text of the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationText',
    @level2type = N'COLUMN',
    @level2name = N'BodyText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the document is supposed to be used as an abstract',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationText',
    @level2type = N'COLUMN',
    @level2name = N'AbstractFlag'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationText] TO [web-p2rmis]
    AS [dbo];