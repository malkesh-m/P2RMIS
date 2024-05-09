CREATE TABLE [dbo].[UserSystemRole] (
    [UserSystemRoleID] INT              IDENTITY (1, 1) NOT NULL,
    [UserID]           INT              NOT NULL,
    [SystemRoleId] INT NOT NULL,
    [CreatedBy]        INT              NULL,
    [CreatedDate]      DATETIME         NULL,
    [ModifiedBy]       INT              NULL,
    [ModifiedDate]     DATETIME         NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL,  
    CONSTRAINT [PK_UserSystemRole] PRIMARY KEY CLUSTERED ([UserSystemRoleID] ASC),
    CONSTRAINT [FK_UserSystemRole_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserSystemRole_SystemRole] FOREIGN KEY (SystemRoleId) REFERENCES [SystemRole]([SystemRoleId])
);

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserSystemRole] TO [web-p2rmis]
    AS [dbo];
GO

GO

CREATE INDEX [IX_UserSystemRole_UserID] ON [dbo].[UserSystemRole] ([UserID])
