CREATE TABLE [dbo].[AccountStatus]
(
	[AccountStatusId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccountStatusName] VARCHAR(30) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an account status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountStatus',
    @level2type = N'COLUMN',
    @level2name = N'AccountStatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of an account status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountStatus',
    @level2type = N'COLUMN',
    @level2name = N'AccountStatusName'

GO
GRANT SELECT
    ON OBJECT::[dbo].[AccountStatus] TO [web-p2rmis]
    AS [dbo];