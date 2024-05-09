CREATE TABLE [dbo].[ClientDataDeliverable]
(
	[ClientDataDeliverableId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [Label] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(500) NOT NULL, 
    [ApiMethod] VARCHAR(100) NOT NULL, 
    [FileFormat] VARCHAR(50) NOT NULL, 
    [QcRequiredFlag] BIT NOT NULL DEFAULT 1,
	[SortOrder] INT NOT NULL DEFAULT 0
    CONSTRAINT [FK_ClientDataDeliverable_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort order when displaying data deliverable items',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether QC is required to download the data deliverable',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'QcRequiredFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Format of the data deliverable',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'FileFormat'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'API method used to construct the deliverable',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'ApiMethod'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of what the deliverable is',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label for the deliverable',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'Label'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client data deliverable',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientDataDeliverable',
    @level2type = N'COLUMN',
    @level2name = N'ClientDataDeliverableId'