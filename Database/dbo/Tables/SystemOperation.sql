CREATE TABLE [dbo].[SystemOperation]
(
	[SystemOperationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OperationName] VARCHAR(100) NOT NULL, 
    [OperationDescription] VARCHAR(1000) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system operation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemOperation',
    @level2type = N'COLUMN',
    @level2name = N'SystemOperationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of an operation permission',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemOperation',
    @level2type = N'COLUMN',
    @level2name = N'OperationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of an operation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemOperation',
    @level2type = N'COLUMN',
    @level2name = N'OperationDescription'