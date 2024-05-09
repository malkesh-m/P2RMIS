CREATE TABLE [dbo].[RoleParticipantType]
(
	[RoleParticipantTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SystemRoleId] INT NOT NULL, 
    [ClientId] INT NOT NULL, 
    [ClientParticipantTypeId] INT NOT NULL, 
    CONSTRAINT [FK_RoleParticipantType_SystemRole] FOREIGN KEY ([SystemRoleId]) REFERENCES [SystemRole]([SystemRoleId]), 
    CONSTRAINT [FK_RoleParticipantType_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_RoleParticipantType_ClientParticipantType] FOREIGN KEY ([ClientParticipantTypeId]) REFERENCES [ClientParticipantType]([ClientParticipantTypeId]), 
    CONSTRAINT [UN_RoleParticipantType_SystemRole_Client] UNIQUE ([SystemRoleId],[ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for role panel participant type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'RoleParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User''s system role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'SystemRoleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Panel''s associated client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Default participation type for role when assigned as a panel participant',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleParticipantType',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Contains mappings of roles to participant types, to be used for automatic assignment or defaulting of participant types given the users role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleParticipantType',
    @level2type = NULL,
    @level2name = NULL