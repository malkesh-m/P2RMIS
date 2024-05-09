CREATE TABLE [dbo].[UserAccountStatusChangeLog]
(
	[UserAccountStatusChangeLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [NewAccountStatusId] INT NULL, 
    [OldAccountStatusId] INT NULL, 
    [NewAccountStatusReasonId] INT NULL, 
    [OldAccountStatusReasonId] INT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserAccountStatusChangeLog_AccountStatusReason] FOREIGN KEY ([NewAccountStatusReasonId]) REFERENCES [AccountStatusReason]([AccountStatusReasonId]), 
    CONSTRAINT [FK_UserAccountStatusChangeLog_OldAccountStatusReason] FOREIGN KEY ([OldAccountStatusReasonId]) REFERENCES AccountStatusReason([AccountStatusReasonId]), 
    CONSTRAINT [FK_UserAccountStatusChangeLog_AccountStatus] FOREIGN KEY ([NewAccountStatusId]) REFERENCES AccountStatus([AccountStatusId]), 
    CONSTRAINT [FK_UserAccountStatusChangeLog_OldAccountStatus] FOREIGN KEY ([OldAccountStatusId]) REFERENCES [AccountStatus]([AccountStatusId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an instance of a user change event',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountStatusChangeLog',
    @level2type = N'COLUMN',
    @level2name = 'UserAccountStatusChangeLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user account',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountStatusChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'New value AccountStatusId (if applicable)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountStatusChangeLog',
    @level2type = N'COLUMN',
    @level2name = 'NewAccountStatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Old value of AccountStatusId (if applicable)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountStatusChangeLog',
    @level2type = N'COLUMN',
    @level2name = 'OldAccountStatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Log of changes to a user''s identifying or account information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountStatusChangeLog',
    @level2type = NULL,
    @level2name = NULL
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Old value of AccountStatusReasonId (if applicable)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountStatusChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'OldAccountStatusReasonId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'New value of AccountStatusReasonId (if applicable)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountStatusChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'NewAccountStatusReasonId'
GO

CREATE INDEX [IX_UserAccountStatusChangeLog_UserId] ON [dbo].[UserAccountStatusChangeLog] ([UserId])
