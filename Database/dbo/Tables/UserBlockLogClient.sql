CREATE TABLE [dbo].[UserBlockLogClient]
(
	[UserBlockLogClientId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserBlockLogId] INT NOT NULL, 
    [ClientId] INT NOT NULL, 
    [BlockFlag] BIT NOT NULL DEFAULT 1,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserBlockLogClient_UserBlockLogId] FOREIGN KEY ([UserBlockLogId]) REFERENCES [UserBlockLog]([UserBlockLogId]), 
    CONSTRAINT [FK_UserBlockLogClient_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user block log client instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLogClient',
    @level2type = N'COLUMN',
    @level2name = N'UserBlockLogClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user block log intance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLogClient',
    @level2type = N'COLUMN',
    @level2name = N'UserBlockLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client/customer/tenant',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLogClient',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the user is blocked/unblocked by this log instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserBlockLogClient',
    @level2type = N'COLUMN',
    @level2name = N'BlockFlag'
GO

CREATE UNIQUE INDEX [IX_UserBlockLogClient_UserBlockLog_Client] ON [dbo].[UserBlockLogClient] ([UserBlockLogId],[ClientId]) WHERE [DeletedFlag] = 0
