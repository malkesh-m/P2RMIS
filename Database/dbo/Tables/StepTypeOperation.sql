CREATE TABLE [dbo].[StepTypeOperation]
(
	[StepTypeOperationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StepTypeId] INT NOT NULL, 
    [SystemOperationId] INT NOT NULL, 
    CONSTRAINT [FK_StepTypeOperation_StepType] FOREIGN KEY ([StepTypeId]) REFERENCES [StepType]([StepTypeId]), 
    CONSTRAINT [FK_StepTypeOperation_SystemOperation] FOREIGN KEY ([SystemOperationId]) REFERENCES [SystemOperation]([SystemOperationId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a step type operation permission',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepTypeOperation',
    @level2type = N'COLUMN',
    @level2name = N'StepTypeOperationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a step type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepTypeOperation',
    @level2type = N'COLUMN',
    @level2name = N'StepTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system operation permission',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'StepTypeOperation',
    @level2type = N'COLUMN',
    @level2name = N'SystemOperationId'