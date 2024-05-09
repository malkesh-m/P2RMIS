CREATE TABLE [dbo].[UserAlternateContact]
(
	[UserAlternateContactId] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserInfoId] INT NOT NULL, 
    [AlternateContactTypeId] INT NOT NULL, 
    [PrimaryFlag] BIT NOT NULL DEFAULT 1, 
    [FirstName] VARCHAR(25) NULL, 
    [LastName] VARCHAR(50) NULL,  
    [EmailAddress] VARCHAR(200) NULL,
    [ProfessionalAffiliationId] INT NULL, 
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserAlternateContact_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo](UserInfoId), 
    CONSTRAINT [FK_UserAlternateContact_AlternateType] FOREIGN KEY ([AlternateContactTypeId]) REFERENCES AlternateContactType(AlternateContactTypeId), 
)

GO

CREATE INDEX [IX_UserAlternateContact_UserInfoId] ON [dbo].[UserAlternateContact] ([UserInfoId]) WHERE (DeletedFlag = 0)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Alternate Contact identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'UserAlternateContactId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Info identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Alternate Contact Type identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'AlternateContactTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Contact''s first name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'FirstName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'contact''s last name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'LastName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'email address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'EmailAddress'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'soft delete (1 = true, 0 = false)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'DeletedFlag'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary or Secondary',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = 'PrimaryFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Professional Afiliation identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAlternateContact',
    @level2type = N'COLUMN',
    @level2name = N'ProfessionalAffiliationId'