CREATE TABLE [dbo].[UserVendor]
(
	[UserVendorId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserInfoId] INT NOT NULL,
    [VendorId] NVARCHAR(10) NOT NULL, 
    [VendorName] NVARCHAR(100) NOT NULL, 
    [ActiveFlag] BIT NOT NULL DEFAULT 1, 
    [VendorTypeId] INT NOT NULL DEFAULT 1,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserVendor_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]), 
    CONSTRAINT [FK_UserVendor_VendorType] FOREIGN KEY ([VendorTypeId]) REFERENCES [VendorType]([VendorTypeId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Surrogate key for user''s vendor entry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserVendor',
    @level2type = N'COLUMN',
    @level2name = N'UserVendorId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Vendor identifier for accounting purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserVendor',
    @level2type = N'COLUMN',
    @level2name = N'VendorId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Vendor namefor accounting purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserVendor',
    @level2type = N'COLUMN',
    @level2name = N'VendorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether this vendor entry is active for the user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserVendor',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s profile information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserVendor',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a vendor type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserVendor',
    @level2type = N'COLUMN',
    @level2name = N'VendorTypeId'
GO

CREATE INDEX [IX_UserVendor_UserInfoId_ActiveFlag] ON [dbo].[UserVendor] ([UserInfoId],[ActiveFlag]) WHERE DeletedFlag = 0
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserVendor] TO [web-p2rmis]
    AS [dbo];