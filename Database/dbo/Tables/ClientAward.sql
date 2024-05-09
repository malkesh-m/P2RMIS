CREATE TABLE [dbo].[ClientAwardType]
(
	[ClientAwardTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientId] INT NOT NULL,
	[LegacyAwardTypeId] VARCHAR(10) NULL DEFAULT CAST(next value for seq_ClientAwardType_LegacyAwardTypeId AS varchar(10)),
	[ParentAwardTypeId] INT NULL,
	[MechanismRelationshipTypeId] INT NULL,
    [AwardAbbreviation] VARCHAR(100) NOT NULL, 
    [AwardDescription] VARCHAR(200) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ClientAwardType_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_ClientAwardType_ClientAwardType] FOREIGN KEY ([ParentAwardTypeId]) REFERENCES [ClientAwardType]([ClientAwardTypeId]), 
    CONSTRAINT [FK_ClientAwardType_MechanismRelationshipType] FOREIGN KEY ([MechanismRelationshipTypeId]) REFERENCES [MechanismRelationshipType]([MechanismRelationshipTypeId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an award offered by a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = N'COLUMN',
    @level2name = N'ClientAwardTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for an award type for mapping.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = N'COLUMN',
    @level2name = N'LegacyAwardTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation of an award.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = N'COLUMN',
    @level2name = N'AwardAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Full name for an award.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = N'COLUMN',
    @level2name = N'AwardDescription'
GO

CREATE INDEX [IX_ClientAwardType_ClientId_AwardAbbreviation] ON [dbo].[ClientAwardType] ([ClientId], [AwardAbbreviation])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Awards that can be offered by a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientAwardType] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an award that may have multiple sub types (e.g. Pre-App)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = N'COLUMN',
    @level2name = N'ParentAwardTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of relationship award has with it''s parent',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAwardType',
    @level2type = N'COLUMN',
    @level2name = N'MechanismRelationshipTypeId'
GO

CREATE NONCLUSTERED INDEX [IX_ClientAwardType_ClientAwardTypeId_DeletedF] ON [dbo].[ClientAwardType]
(
	[ClientAwardTypeId] ASC,
	[DeletedFlag] ASC
)
INCLUDE ( 	[AwardAbbreviation]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
