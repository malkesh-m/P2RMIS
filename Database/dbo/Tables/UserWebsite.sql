CREATE TABLE [dbo].[UserWebsite]
(
	[UserWebsiteId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserInfoId] INT NOT NULL, 
	[WebsiteTypeId] INT NOT NULL,  
	[WebsiteAddress] VARCHAR(500) NOT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserWebsite_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoID]), 
    CONSTRAINT [FK_UserWebsite_WebsiteType] FOREIGN KEY ([WebsiteTypeId]) REFERENCES [WebsiteType]([WebsiteTypeId]) 

)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User Website Identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserWebsite',
    @level2type = N'COLUMN',
    @level2name = N'UserWebsiteId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'UserInfo identifier of the assoicatied user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserWebsite',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Website Address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserWebsite',
    @level2type = N'COLUMN',
    @level2name = N'WebsiteAddress'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1 (true) = soft delete',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserWebsite',
    @level2type = N'COLUMN',
    @level2name = N'DeletedFlag'


GO

GO


CREATE INDEX [IX_UserWebsite_UserInfoId] ON [dbo].[UserWebsite] ([UserInfoId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Website type (Primary or Secondary)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserWebsite',
    @level2type = N'COLUMN',
    @level2name = 'WebsiteTypeId'