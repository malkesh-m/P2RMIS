CREATE TABLE [dbo].[TaskOperation]
(
	[TaskOperationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SystemTaskId] INT NOT NULL, 
    [SystemOperationId] INT NOT NULL, 
    CONSTRAINT [FK_TaskOperation_SystemTask] FOREIGN KEY ([SystemTaskId]) REFERENCES [SystemTask]([SystemTaskId]), 
    CONSTRAINT [FK_TaskOperation_SystemOperation] FOREIGN KEY ([SystemOperationId]) REFERENCES [SystemOperation]([SystemOperationId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a tasks operation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TaskOperation',
    @level2type = N'COLUMN',
    @level2name = N'TaskOperationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system task',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TaskOperation',
    @level2type = N'COLUMN',
    @level2name = 'SystemTaskId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system operation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TaskOperation',
    @level2type = N'COLUMN',
    @level2name = 'SystemOperationId'