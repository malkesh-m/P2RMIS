CREATE TABLE [dbo].[ClientApplicationInfoType]
(
	[ClientApplicationInfoTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [InfoTypeDescription] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a type of application information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationInfoType',
    @level2type = N'COLUMN',
    @level2name = N'ClientApplicationInfoTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationInfoType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name describing the nature of the application information type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationInfoType',
    @level2type = N'COLUMN',
    @level2name = N'InfoTypeDescription'

GO
GRANT SELECT
    ON OBJECT::[dbo].[ClientApplicationInfoType] TO [web-p2rmis]
    AS [dbo];