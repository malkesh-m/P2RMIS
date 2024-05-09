CREATE TABLE [dbo].[UserClientBlock]
(
	[UserClientBlockId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserInfoId] INT NOT NULL, 
    [ClientId] INT NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserClientBlock_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]), 
    CONSTRAINT [FK_UserClientBlock_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user client block instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserClientBlock',
    @level2type = N'COLUMN',
    @level2name = N'UserClientBlockId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserClientBlock',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client/customer/tenant',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserClientBlock',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO

CREATE UNIQUE INDEX [UIX_UserClientBlock_UserInfoId_ClientId] ON [dbo].[UserClientBlock] ([UserInfoId],[ClientId]) WHERE [DeletedFlag] = 0
