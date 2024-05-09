CREATE TABLE [dbo].[RoleTask]
(
	[RoleTaskId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SystemRoleId] INT NOT NULL, 
    [SystemTaskId] INT NOT NULL, 
    CONSTRAINT [FK_RoleTask_SystemRole] FOREIGN KEY ([SystemRoleId]) REFERENCES [SystemRole]([SystemRoleId]), 
    CONSTRAINT [FK_RoleTask_SystemTask] FOREIGN KEY ([SystemTaskId]) REFERENCES [SystemTask]([SystemTaskId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a role task relationship',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleTask',
    @level2type = N'COLUMN',
    @level2name = N'RoleTaskId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleTask',
    @level2type = N'COLUMN',
    @level2name = N'SystemRoleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a task permission definition',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RoleTask',
    @level2type = N'COLUMN',
    @level2name = N'SystemTaskId'