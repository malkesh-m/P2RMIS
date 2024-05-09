CREATE TABLE [dbo].[User] (
    [UserID]               INT              IDENTITY (1, 1) NOT NULL,
    [UserLogin]            NVARCHAR (100)   NULL,
    [Password]             VARCHAR (128)    NULL,
    [PasswordDate]         DATETIME         NULL,
    [PasswordSalt]         VARCHAR (128)    NULL,
    [Verified]             BIT              CONSTRAINT [DF_User_Verified] DEFAULT ((0)) NULL,
    [VerifiedDate]         DATETIME         NULL,
    [IsActivated]          BIT              CONSTRAINT [DF_User_IsActivated] DEFAULT ((0)) NOT NULL,
    [LastLoginDate]        DATETIME          NOT NULL DEFAULT ('6/1/2012'),
    [IsLockedOut]          BIT              CONSTRAINT [DF_User_IsLockedOut] DEFAULT ((0)) NOT NULL,
    [LastLockedOutDate]    DATETIME          NOT NULL DEFAULT ('6/1/2012'),
    [NewPasswordRequested] DATETIME          NULL,
    [NewEmail]             NVARCHAR (100)   NULL,
    [NewEmailRequested]    DATETIME          NULL,
    [PersonID]             INT              NULL,
	[CredentialSentBy]	   INT				NULL,
	[CredentialSentDate]   datetime2(0)	NULL,
	[W9Verified]		   BIT NULL DEFAULT null, 
    [W9VerifiedDate]       datetime2(0) NULL, 
	[CreatedBy]            INT              NULL,
    [CreatedDate]          DATETIME          NOT NULL DEFAULT ('6/1/2012'),
    [ModifiedBy]           INT              NULL,
    [ModifiedDate]         DATETIME         NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [PK_tbl_users] PRIMARY KEY CLUSTERED ([UserID] ASC),
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy person identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'PersonID'
GO

GO

GO

GO

GO

GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Deprecated: Not Used',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'NewEmail'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unknown',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'NewEmailRequested'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date new password was requested.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'NewPasswordRequested'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date user was last locked out. Not currently used except in membership repository.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'LastLockedOutDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'If user is locked out currently. Not currently used.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'IsLockedOut'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date of last user login. Defaulted for purposes of membership repository.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'LastLoginDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'If the users account is active and can log in',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'IsActivated'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the users personal information has been verified',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'Verified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date users personal information was last verified',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'VerifiedDate'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Encrypted password',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'Password'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Salt for password encryption',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'PasswordSalt'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date password was last set',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'PasswordDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'UserLogin'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for a User',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'UserID'

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores information related to a user system account',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = NULL,
    @level2name = NULL
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[User] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'true if w9 is verified accurate by reviewer, false if verified inaccurate by reviewer, null when W9 first entered',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'W9Verified'

GO

CREATE INDEX [IX_User_UserLogin] ON [dbo].[User] ([UserLogin]) INCLUDE ([Password],[PasswordSalt],[UserID])
