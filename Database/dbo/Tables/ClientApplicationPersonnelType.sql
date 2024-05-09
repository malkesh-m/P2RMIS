CREATE TABLE [dbo].[ClientApplicationPersonnelType]
(
	[ClientApplicationPersonnelTypeId] INT NOT NULL IDENTITY , 
	[ClientId] INT NOT NULL ,
    [ApplicationPersonnelType] VARCHAR(50) NOT NULL, 
	[ApplicationPersonnelTypeAbbreviation] VARCHAR(10) NULL,
    [CoiFlag] BIT NOT NULL DEFAULT 1, 
    [ExternalPersonnelTypeId] INT NULL, 
    CONSTRAINT [PK_ClientApplicationPersonnelType] PRIMARY KEY ([ClientApplicationPersonnelTypeId]), 
    CONSTRAINT [FK_ClientApplicationPersonnelType_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application personnel type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationPersonnelType',
    @level2type = N'COLUMN',
    @level2name = N'ClientApplicationPersonnelTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationPersonnelType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name describing the personnel type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationPersonnelType',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationPersonnelType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for the personnel type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationPersonnelType',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationPersonnelTypeAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the personnel type is considered to be a conflict if on a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationPersonnelType',
    @level2type = N'COLUMN',
    @level2name = N'CoiFlag'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientApplicationPersonnelType] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'An external identifier for mapping personne typesl from source systems',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientApplicationPersonnelType',
    @level2type = N'COLUMN',
    @level2name = N'ExternalPersonnelTypeId'