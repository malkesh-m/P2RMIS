CREATE TABLE [dbo].[UserProfile]
(
	[UserProfileId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserInfoId] INT NOT NULL,
	[ProfileTypeId] INT NOT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserProfile_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]), 
    CONSTRAINT [FK_UserProfile_ProfileType] FOREIGN KEY ([ProfileTypeId]) REFERENCES [ProfileType]([ProfileTypeId]), 
    CONSTRAINT [UN_UserProfile_UserInfoId] UNIQUE ([UserInfoId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Profile Identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserProfile',
    @level2type = N'COLUMN',
    @level2name = N'UserProfileId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Information foreign key identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserProfile',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Profile Type foreign key identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserProfile',
    @level2type = N'COLUMN',
    @level2name = N'ProfileTypeId'
GO



EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User correlation with profile, info and profile type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserProfile',
    @level2type = NULL,
    @level2name = NULL