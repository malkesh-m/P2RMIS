CREATE TABLE [dbo].[ContractStatus]
(
	[ContractStatusId] INT NOT NULL PRIMARY KEY, 
    [ActionLabel] VARCHAR(20) NULL,
    [StatusLabel] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL, 
    [UserActionFlag] BIT NOT NULL DEFAULT 1
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the related status action can be performed by system users',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ContractStatus',
    @level2type = N'COLUMN',
    @level2name = N'UserActionFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the status action is displayed in dropdowns',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ContractStatus',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label used for display of assigned status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ContractStatus',
    @level2type = N'COLUMN',
    @level2name = N'StatusLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label used for display of related status actions',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ContractStatus',
    @level2type = N'COLUMN',
    @level2name = N'ActionLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a contract status type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ContractStatus',
    @level2type = N'COLUMN',
    @level2name = N'ContractStatusId'