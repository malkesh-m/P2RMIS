CREATE TABLE [dbo].[UserPassword]
(
    [UserPasswordID]               INT              IDENTITY (1, 1) NOT NULL,
    [UserID] INT NOT NULL, 
    [Password] VARCHAR(128) NOT NULL, 
    [PasswordDate] DATETIME NULL, 
    [PasswordSalt] VARCHAR(128) NOT NULL,
    [CreatedBy]        INT              NULL,
    [CreatedDate]      DATETIME         NULL,
    [ModifiedBy]       INT              NULL,
    [ModifiedDate]     DATETIME         NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL,
    CONSTRAINT [PK_UserPassword] PRIMARY KEY CLUSTERED ([UserPasswordID] ASC), 
    CONSTRAINT [FK_UserPassword_User] FOREIGN KEY ([UserID]) REFERENCES [User]([UserID]),
)

GO 
CREATE INDEX [IX_UserPassword_UserId_PasswordDate] ON [dbo].[UserPassword] ([UserId], [PasswordDate] desc)