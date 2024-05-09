CREATE TABLE [dbo].[PolicyNetworkRange]
(
	[PolicyNetworkRangeID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PolicyID] INT NOT NULL,
	[StartAddress] VARCHAR(20) NOT NULL, 
    [EndAddress] VARCHAR(20) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PolicyNetworkRange_Policy] FOREIGN KEY ([PolicyID]) REFERENCES [Policy]([PolicyId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Key to store Network Range Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRange',
    @level2type = N'COLUMN',
    @level2name = N'PolicyNetworkRangeID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Id for network range',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRange',
    @level2type = N'COLUMN',
    @level2name = N'PolicyID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Start IP Address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRange',
    @level2type = N'COLUMN',
    @level2name = N'StartAddress'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Ending IP Address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyNetworkRange',
    @level2type = N'COLUMN',
    @level2name = N'EndAddress'