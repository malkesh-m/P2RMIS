CREATE TABLE [dbo].[AccountStatusReason]
(
	[AccountStatusReasonId] INT NOT NULL PRIMARY KEY, 
    [AccountStatusReasonName] VARCHAR(30) NOT NULL, 
    [AccountStatusId] INT NOT NULL, 
    CONSTRAINT [FK_AccountStatusReason_AccountStatus] FOREIGN KEY ([AccountStatusId]) REFERENCES [AccountStatus]([AccountStatusId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the reason why a user can login or not',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountStatusReason',
    @level2type = N'COLUMN',
    @level2name = N'AccountStatusReasonId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Value displayed for why a user can login or not',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountStatusReason',
    @level2type = N'COLUMN',
    @level2name = N'AccountStatusReasonName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Possible reasons for an account status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountStatusReason',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'AccountStatusId associated with the reason',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountStatusReason',
    @level2type = N'COLUMN',
    @level2name = N'AccountStatusId'