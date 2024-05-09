CREATE TABLE [dbo].[ClientCoiType]
(
	[ClientCoiTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [CoiTypeName] VARCHAR(100) NOT NULL, 
    [CoiTypeDescription] VARCHAR(8000) NOT NULL, 
    CONSTRAINT [FK_ClientCoiType_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a COI type for client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientCoiType',
    @level2type = N'COLUMN',
    @level2name = N'ClientCoiTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientCoiType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the COI type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientCoiType',
    @level2type = N'COLUMN',
    @level2name = N'CoiTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of the COI type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientCoiType',
    @level2type = N'COLUMN',
    @level2name = N'CoiTypeDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Conflict of interest categories specified by a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientCoiType',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[ClientCoiType] TO [web-p2rmis]
    AS [dbo];