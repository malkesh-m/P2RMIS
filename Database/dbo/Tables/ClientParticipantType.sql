CREATE TABLE [dbo].[ClientParticipantType]
(
	[ClientParticipantTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientId] INT NOT NULL,
	[LegacyPartTypeId]  VARCHAR(10) NULL,
    [ParticipantTypeAbbreviation] VARCHAR(8) NOT NULL,
	[ParticipantTypeName] VARCHAR(50) NOT NULL, 
	[ParticipantScope] VARCHAR(10) NOT NULL,
	[ActiveFlag] BIT NOT NULL DEFAULT 1,
	[ReviewerFlag] BIT NOT NULL DEFAULT 1,
	[ChairpersonFlag] BIT NOT NULL DEFAULT 0,
	[ElevatedChairpersonFlag] BIT NOT NULL DEFAULT 0,
	[ConsumerFlag] BIT NOT NULL DEFAULT 0,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [SROFlag] BIT NULL DEFAULT 0, 
    [RTAFlag] BIT NULL DEFAULT 0, 
    CONSTRAINT [FK_ClientParticipantType_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),


)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Scope of the participation (Program, Panel)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ParticipantScope'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client''s participant types',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviated display of a participant type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ParticipantTypeAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Full name of a participant type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ParticipantTypeName'
GO


CREATE INDEX [IX_ClientParticipantType_ClientId_ParticipantTypeAbbreviation] ON [dbo].[ClientParticipantType] ([ClientId], [ParticipantTypeAbbreviation])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Potential participation types for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the participant type is available for user assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the participant type actually reviews applications on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ReviewerFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a participant type for mapping',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'LegacyPartTypeId'
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the pariticpant type acts as a chairperson for the review',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ChairpersonFlag'
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the pariticpant type acts as a consumer for the review',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ConsumerFlag'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientParticipantType] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the participant type acts as a chair on the panel with elevated access',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ElevatedChairpersonFlag'