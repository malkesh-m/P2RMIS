CREATE TABLE [dbo].[SystemTask]
(
	[SystemTaskId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TaskName] VARCHAR(100) NOT NULL, 
    [TaskDescription] VARCHAR(1000) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system task permission',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemTask',
    @level2type = N'COLUMN',
    @level2name = N'SystemTaskId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the system task',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemTask',
    @level2type = N'COLUMN',
    @level2name = N'TaskName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of the task',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemTask',
    @level2type = N'COLUMN',
    @level2name = N'TaskDescription'