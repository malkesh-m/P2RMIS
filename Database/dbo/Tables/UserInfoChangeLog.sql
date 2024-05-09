CREATE TABLE [dbo].[UserInfoChangeLog]
(
	[UserInfoChangeLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserInfoId] INT NOT NULL, 
	[UserInfoChangeTypeId] INT NOT NULL,
    [OldValue] VARCHAR(4000) NULL, 
    [NewValue] VARCHAR(4000) NULL, 
	[Identifier] INT NOT NULL DEFAULT 0,
	[ReviewedFlag] BIT NOT NULL DEFAULT 0,
    [ReviewedDate] DATETIME2(0) NULL, 
    [ReviewedBy] INT NULL, 
    	[CreatedBy] INT NULL, 
    [CreatedDate] DATETIME2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] DATETIME2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] DATETIME2(0) NULL, 
    CONSTRAINT [FK_UserInfoChangeLog_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]), 
    CONSTRAINT [FK_UserInfoChangeLog_UserInfoChangeType] FOREIGN KEY ([UserInfoChangeTypeId]) REFERENCES [UserInfoChangeType]([UserInfoChangeTypeId]), 
    CONSTRAINT [FK_UserInfoChangeLog_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserInfoChangeLog_User_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [User]([UserId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user info change event',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoChangeLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User information identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Previous value of user information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'OldValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'New value of user information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'NewValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the change has been reviewed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'ReviewedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the change was reviewed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'ReviewedDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User who reviewed the change',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'ReviewedBy'
GO

CREATE INDEX [IX_UserInfoChangeLog_ReviewedDate_DeletedFlag_UserInfoId] ON [dbo].[UserInfoChangeLog] ([ReviewedDate],[DeletedFlag], [UserInfoId])

GO

CREATE INDEX [IX_UserInfoChangeLog_UserInfoId_ReviewedDate_DeletedFlag] ON [dbo].[UserInfoChangeLog] ([UserInfoId], [ReviewedDate], [DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type of user information changed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoChangeTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Database key value for the data change in source table',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeLog',
    @level2type = N'COLUMN',
    @level2name = N'Identifier'
GO

CREATE INDEX [IX_UserInfoChangeLog_Identifier] ON [dbo].[UserInfoChangeLog] ([Identifier], [DeletedFlag])

