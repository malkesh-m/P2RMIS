CREATE TABLE [dbo].[UserClient] (
    [UserClientID] INT IDENTITY (1, 1) NOT NULL,
    [UserID]       INT NOT NULL,
    [ClientID]     INT NOT NULL,
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [PK_UserClient] PRIMARY KEY CLUSTERED ([UserClientID] ASC),
    CONSTRAINT [FK_UserClient_Client] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Client] ([ClientID]),
    CONSTRAINT [FK_UserClient_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]) ON DELETE CASCADE
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s client assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserClient',
    @level2type = N'COLUMN',
    @level2name = N'UserClientID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserClient',
    @level2type = N'COLUMN',
    @level2name = N'UserID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserClient',
    @level2type = N'COLUMN',
    @level2name = N'ClientID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Which client''s information a user should have access to',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserClient',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedBy'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserClient] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_UserClient_UserID_ClientID] ON [dbo].[UserClient] ([UserID],[ClientID]) WHERE ([DeletedFlag] = 0)

GO

CREATE INDEX [IX_UserClient_ClientID] ON [dbo].[UserClient] ([ClientID]) INCLUDE ([UserID]) WHERE ([DeletedFlag] = 0)
