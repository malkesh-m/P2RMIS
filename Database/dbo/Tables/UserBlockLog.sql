CREATE TABLE [dbo].[UserBlockLog]
(
	[UserBlockLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserInfoId] INT NOT NULL, 
	[EnteredByUserId] INT NOT NULL,
    [BlockComment] VARCHAR(MAX) NOT NULL, 
    [CreatedBy]        INT              NULL,
    [CreatedDate]      DATETIME         NULL,
    [ModifiedBy]       INT              NULL,
    [ModifiedDate]     DATETIME         NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserBlockLog_UserInfoId] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]), 
    CONSTRAINT [FK_UserBlockLog_EnteredByUserId] FOREIGN KEY ([EnteredByUserId]) REFERENCES [User]([UserId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user block log instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLog',
    @level2type = N'COLUMN',
    @level2name = N'UserBlockLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s information the block is regarding',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLog',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User identifier for individual who added the block entry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLog',
    @level2type = N'COLUMN',
    @level2name = N'EnteredByUserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description regarding context of block',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLog',
    @level2type = N'COLUMN',
    @level2name = N'BlockComment'
GO

CREATE INDEX [IX_UserBlockLog_UserInfoId_EnteredByUserId] ON [dbo].[UserBlockLog] ([EnteredByUserId]) WHERE DeletedFlag = 0
