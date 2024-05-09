CREATE TABLE [dbo].[UserCommunicationLog]
(
	[UserCommunicationLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [CommunicationMethodId] INT NOT NULL, 
    [Comment] VARCHAR(500) NOT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] SMALLDATETIME NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] SMALLDATETIME NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] SMALLDATETIME NULL, 
    CONSTRAINT [FK_UserCommunicationLog_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserCommunicationLog_CommunicationMethod] FOREIGN KEY ([CommunicationMethodId]) REFERENCES [CommunicationMethod]([CommunicationMethodId]), 
)

GO

CREATE INDEX [IX_UserCommunicationLog_UserId_DeletedFlag] ON [dbo].[UserCommunicationLog] ([UserId],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a communication log entry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserCommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'UserCommunicationLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user account',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserCommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a communication method',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserCommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationMethodId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comment describing the communication outcome',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserCommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'Comment'