CREATE TABLE [dbo].[UserAlternateContactPhone]
(
	[UserAlternateContactPhoneId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserAlternateContactId] INT NOT NULL,
    [PhoneTypeId] INT NOT NULL,
	[PrimaryFlag] BIT NOT NULL DEFAULT 0, 
    [PhoneNumber] VARCHAR(50) NOT NULL, 
    [PhoneExtension] VARCHAR(50) NULL, 
    [International] BIT NOT NULL Default 0,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL,  
    CONSTRAINT [FK_UserAlternateContactPhone_PhoneType] FOREIGN KEY ([PhoneTypeId]) REFERENCES PhoneType([PhoneTypeId]), 
    CONSTRAINT [FK_UserAlternateContactPhone_UserAlternateContact] FOREIGN KEY (UserAlternateContactId) REFERENCES UserAlternateContact([UserAlternateContactId]),

)

GO

CREATE INDEX [IX_UserAlternateContactPhone_UserAlternateContactId] ON [dbo].[UserAlternateContactPhone] ([UserAlternateContactId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Alternate Contact Phone identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'UserAlternateContactPhoneId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Alternate Contact Identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'UserAlternateContactId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Phone type Identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'PhoneTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Phone Number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'PhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Phone Extension',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'PhoneExtension'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'International',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'International'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'soft delete',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'DeletedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'true indicates primary',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContactPhone',
    @level2type = N'COLUMN',
    @level2name = N'PrimaryFlag'