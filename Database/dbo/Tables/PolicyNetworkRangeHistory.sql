CREATE TABLE [dbo].[PolicyNetworkRangeHistory]
(
	[PolicyNetworkRangeHistoryId] INT NOT NULL PRIMARY KEY,
    [PolicyHistoryId] INT,
	[PolicyId] INT NOT NULL,
    [VersionId] INT NOT NULL,
	[StartAddress] VARCHAR(20) NOT NULL, 
    [EndAddress] VARCHAR(20) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL,
    CONSTRAINT [FK_PolicyNetworkRangeHistory_PolicyHistory] FOREIGN KEY ([PolicyHistoryId]) REFERENCES [PolicyHistory]([PolicyHistoryId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Key to Store Network Range History id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRangeHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyNetworkRangeHistoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Histiory id for network range history',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRangeHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyHistoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy to which this range applies',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRangeHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Change version Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRangeHistory',
    @level2type = N'COLUMN',
    @level2name = N'VersionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Range Start IP Address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRangeHistory',
    @level2type = N'COLUMN',
    @level2name = N'StartAddress'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Range End IP Address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRangeHistory',
    @level2type = N'COLUMN',
    @level2name = N'EndAddress'