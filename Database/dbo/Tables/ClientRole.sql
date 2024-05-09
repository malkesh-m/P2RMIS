CREATE TABLE [dbo].[ClientRole]
(
	[ClientRoleId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientId] INT NOT NULL,
	[LegacyRoleId] INT NULL,
    [RoleAbbreviation] VARCHAR(35) NOT NULL, 
    [RoleName] VARCHAR(50) NOT NULL, 
	[ActiveFlag] BIT NOT NULL DEFAULT 1,
	[SpecialistFlag] BIT NOT NULL DEFAULT 0,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ClientRole_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a participant role for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = N'COLUMN',
    @level2name = N'ClientRoleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a role for mapping',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = N'COLUMN',
    @level2name = N'LegacyRoleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation of a role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = N'COLUMN',
    @level2name = N'RoleAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Full name for a role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = N'COLUMN',
    @level2name = N'RoleName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the role is currently active for the client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client''s available participant roles on a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the role is considered a specialist reviewer',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientRole',
    @level2type = N'COLUMN',
    @level2name = N'SpecialistFlag'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientRole] TO [web-p2rmis]
    AS [dbo];