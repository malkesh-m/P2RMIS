CREATE TABLE [dbo].[UserEmail] (
    [EmailID]      INT              IDENTITY (1, 1) NOT NULL,
    [UserInfoID]   INT              NOT NULL,
    [EmailAddressTypeId] INT NOT NULL DEFAULT 1, 
    [Email]        NVARCHAR (100)   NOT NULL,
    [PrimaryFlag]    BIT              NOT NULL DEFAULT 1,
    [CreatedBy]    INT              NULL,
    [CreatedDate]  DATETIME         NULL,
    [ModifiedBy]   INT              NULL,
    [ModifiedDate] DATETIME         NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [PK_UserEmail] PRIMARY KEY CLUSTERED ([EmailID] ASC),
    CONSTRAINT [FK_UserEmail_UserInfo] FOREIGN KEY ([UserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserEmail_EmailAddressType] FOREIGN KEY ([EmailAddressTypeId]) REFERENCES [EmailAddressType]([EmailAddressTypeId]) 
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Email addresses of a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEmail',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user email address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEmail',
    @level2type = N'COLUMN',
    @level2name = N'EmailID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a set of user information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEmail',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Email address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEmail',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the email is considered the user''s primary email for communication',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEmail',
    @level2type = N'COLUMN',
    @level2name = 'PrimaryFlag'
GO


CREATE INDEX [IX_UserEmail_UserInfoId] ON [dbo].[UserEmail] ([UserInfoID])

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserEmail] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_UserEmail_EmailAddressTypeId] ON [dbo].[UserEmail] ([EmailAddressTypeId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the Email Address Type, default to business',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEmail',
    @level2type = N'COLUMN',
    @level2name = N'EmailAddressTypeId'

GO

CREATE INDEX [IX_UserEmail_Email_PrimaryFlag] ON [dbo].[UserEmail] ([Email],[PrimaryFlag]) INCLUDE ([UserInfoID], [EmailAddressTypeId]) WHERE (DeletedFlag = 0)
