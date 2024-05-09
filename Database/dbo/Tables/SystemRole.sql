CREATE TABLE [dbo].[SystemRole]
(
	[SystemRoleId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SystemRoleName] VARCHAR(50) NOT NULL, 
    [SystemRoleContext] VARCHAR(50) NOT NULL, 
    [SystemRoleCode] VARCHAR(20) NOT NULL, 
    [SystemPriorityOrder] INT NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'primary key identifier for a role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemRole',
    @level2type = N'COLUMN',
    @level2name = N'SystemRoleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The role name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemRole',
    @level2type = N'COLUMN',
    @level2name = N'SystemRoleName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The context of the role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemRole',
    @level2type = N'COLUMN',
    @level2name = N'SystemRoleContext'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The role code',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemRole',
    @level2type = N'COLUMN',
    @level2name = N'SystemRoleCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The role priority order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemRole',
    @level2type = N'COLUMN',
    @level2name = N'SystemPriorityOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The role sort order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemRole',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'