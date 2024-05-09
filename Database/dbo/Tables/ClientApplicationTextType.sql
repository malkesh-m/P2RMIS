CREATE TABLE [dbo].[ClientApplicationTextType]
(
	[ClientApplicationTextTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientId] INT NOT NULL,
    [TextType] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ClientApplicationTextType_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application text document type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationTextType',
    @level2type = N'COLUMN',
    @level2name = N'ClientApplicationTextTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationTextType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name describing the text document type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationTextType',
    @level2type = N'COLUMN',
    @level2name = N'TextType'

GO
GRANT SELECT
    ON OBJECT::[dbo].[ClientApplicationTextType] TO [web-p2rmis]
    AS [dbo];