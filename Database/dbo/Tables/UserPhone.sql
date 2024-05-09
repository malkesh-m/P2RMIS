CREATE TABLE [dbo].[UserPhone] (
    [PhoneID]       INT              IDENTITY (1, 1) NOT NULL,
    [UserInfoID]    INT              NOT NULL,
	[PhoneTypeId] INT NULL DEFAULT 3,
	[LegacyPhoneId] INT NULL,
    [Phone]         NVARCHAR (50)    NULL,
    [Extension]     NVARCHAR (50)    NULL,
    [PrimaryFlag]     BIT              CONSTRAINT [DF_UserPhone_IsPrimary] DEFAULT 1 NULL,
    [International] BIT              CONSTRAINT [DF_UserPhone_International] DEFAULT 0 NULL,
    [CreatedBy]     INT               NULL,
    [CreatedDate]   DATETIME         NULL,
    [ModifiedBy]    INT              NULL,
    [ModifiedDate]  DATETIME         NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [PK_UserPhone] PRIMARY KEY CLUSTERED ([PhoneID] ASC),
    CONSTRAINT [FK_UserPhone_UserInfo] FOREIGN KEY ([UserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserPhone_PhoneType] FOREIGN KEY ([PhoneTypeId]) REFERENCES [PhoneType]([PhoneTypeId])
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s phone number ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'PhoneID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identiifer for a user''s personal information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the context of the phone number.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'PhoneTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for legacy mapping purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'LegacyPhoneId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Phone number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'Phone'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Extension for a phone number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'Extension'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the phone number is preferred for communication for the user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'PrimaryFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the phone number is an international number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPhone',
    @level2type = N'COLUMN',
    @level2name = N'International'
GO

CREATE INDEX [IX_UserPhone_UserInfoId_PhoneTypeId] ON [dbo].[UserPhone] ([UserInfoId], [PhoneTypeId])

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserPhone] TO [web-p2rmis]
    AS [dbo];