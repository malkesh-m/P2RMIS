CREATE TABLE [dbo].[UserInfoChangeType]
(
	[UserInfoChangeTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Label] VARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL DEFAULT 0, 
    [TableName] VARCHAR(50) NULL, 
    [FieldName] VARCHAR(50) NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user change type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeType',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoChangeTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display value for the change type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeType',
    @level2type = N'COLUMN',
    @level2name = 'Label'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Types of user changes tracked',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeType',
    @level2type = NULL,
    @level2name = NULL
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the change should be displayed if existing for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Table name of the field which changes are tracked',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeType',
    @level2type = N'COLUMN',
    @level2name = N'TableName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Field name in which changes are tracked',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfoChangeType',
    @level2type = N'COLUMN',
    @level2name = N'FieldName'